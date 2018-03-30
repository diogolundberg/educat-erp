using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FinanceDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public FinanceDataController(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = databaseContext;
        }

        [HttpPost("{token}", Name = "ONBOARDING/FINANCEDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]FinanceDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            FinanceData financeData = _context.Set<FinanceData>()
                                              .Include("Enrollment")
                                              .Include("Representative")
                                              .Include("Guarantors")
                                              .Include("Guarantors.GuarantorDocuments")
                                              .Include("Guarantors.GuarantorDocuments.Document")
                                              .SingleOrDefault(x => x.Id == obj.Id);

            if (financeData == null)
            {
                return NotFound();
            }

            if (financeData.Enrollment.SendDate.HasValue || !financeData.Enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            if (obj.Representative is RepresentativePersonViewModel)
            {
                RepresentativePerson representativePerson = _mapper.Map<RepresentativePerson>((RepresentativePersonViewModel)obj.Representative);
                representativePerson.FinanceDataId = financeData.Id;

                if (financeData.Representative == null)
                {
                    _context.Set<RepresentativePerson>().Add(representativePerson);
                }
                else
                {
                    if (financeData.Representative is RepresentativeCompany)
                    {
                        _context.Set<RepresentativeCompany>().Remove((RepresentativeCompany)financeData.Representative);
                        _context.Set<RepresentativePerson>().Add(representativePerson);
                    }
                    else
                    {
                        representativePerson.Id = financeData.Representative.Id;
                        _context.Entry(financeData.Representative).CurrentValues.SetValues(representativePerson);
                    }
                }
            }
            else
            {
                RepresentativeCompany representativeCompany = _mapper.Map<RepresentativeCompany>((RepresentativeCompanyViewModel)obj.Representative);
                representativeCompany.FinanceDataId = financeData.Id;

                if (financeData.Representative == null)
                {
                    _context.Set<RepresentativeCompany>().Add(representativeCompany);
                }
                else
                {
                    if (financeData.Representative is RepresentativePerson)
                    {
                        _context.Set<RepresentativePerson>().Remove((RepresentativePerson)financeData.Representative);
                        _context.Set<RepresentativeCompany>().Add(representativeCompany);
                    }
                    else
                    {
                        representativeCompany.Id = financeData.Representative.Id;
                        _context.Entry(financeData.Representative).CurrentValues.SetValues(representativeCompany);
                    }
                }
            }

            foreach (Guarantor guarantor in financeData.Guarantors.ToList())
            {
                if (!obj
                    .Guarantors
                    .Any(c => c.Id == guarantor.Id))
                {
                    _context.Set<GuarantorDocument>().RemoveRange(guarantor.GuarantorDocuments);
                    _context.Set<Document>().RemoveRange(guarantor.GuarantorDocuments.Select(x => x.Document));
                    _context.Set<Guarantor>().Remove(guarantor);
                }
            }
            foreach (GuarantorViewModel guarantorViewModel in obj.Guarantors)
            {
                Guarantor existingGuarantor = financeData.Guarantors
                    .Where(c => c.Id == guarantorViewModel.Id)
                    .SingleOrDefault();

                if (existingGuarantor != null)
                {
                    Guarantor guarantor = _mapper.Map<Guarantor>(guarantorViewModel);
                    guarantor.Id = existingGuarantor.Id;
                    guarantor.FinanceDataId = financeData.Id;
                    _context.Entry(existingGuarantor).CurrentValues.SetValues(guarantor);

                    foreach (GuarantorDocument guarantorDocument in existingGuarantor.GuarantorDocuments.ToList())
                    {
                        if (!guarantorViewModel
                            .Documents
                            .Any(c => c.Id == guarantorDocument.DocumentId || c.DocumentTypeId == guarantorDocument.Document.DocumentTypeId))
                        {
                            _context.Set<GuarantorDocument>().Remove(guarantorDocument);
                            _context.Set<Document>().Remove(_context.Set<Document>().Find(guarantorDocument.DocumentId));
                        }
                    }
                    foreach (DocumentViewModel guarantorDocumentViewModel in guarantorViewModel.Documents)
                    {
                        GuarantorDocument existingGuarantorDocument = existingGuarantor.GuarantorDocuments
                            .Where(c => c.DocumentId == guarantorDocumentViewModel.Id || c.Document.DocumentTypeId == guarantorDocumentViewModel.DocumentTypeId)
                            .SingleOrDefault();

                        if (existingGuarantorDocument != null)
                        {
                            GuarantorDocument guarantorDocument = new GuarantorDocument
                            {
                                Document = new Document
                                {
                                    Id = existingGuarantorDocument.Document.Id,
                                    Url = guarantorDocumentViewModel.Url,
                                    DocumentTypeId = guarantorDocumentViewModel.DocumentTypeId,
                                },
                                GuarantorId = existingGuarantor.Id
                            };

                            _context.Entry(existingGuarantorDocument.Document).CurrentValues.SetValues(guarantorDocument.Document);
                        }
                        else
                        {
                            GuarantorDocument guarantorDocument = new GuarantorDocument
                            {
                                Document = new Document
                                {
                                    Id = 0,
                                    Url = guarantorDocumentViewModel.Url,
                                    DocumentTypeId = guarantorDocumentViewModel.DocumentTypeId
                                },
                                GuarantorId = existingGuarantor.Id
                            };

                            _context.Set<GuarantorDocument>().Add(guarantorDocument);
                        }
                    }
                }
                else
                {
                    Guarantor guarantor = _mapper.Map<Guarantor>(guarantorViewModel);
                    guarantor.Id = 0;
                    guarantor.FinanceDataId = financeData.Id;
                    _context.Set<Guarantor>().Add(guarantor);
                }
            }

            financeData.PlanId = obj.PlanId;
            financeData.PaymentMethodId = obj.PaymentMethodId;

            _context.SaveChanges();
            _context.Entry(financeData).Reload();

            FinanceDataViewModel viewModel = _mapper.Map<FinanceDataViewModel>(financeData);
            viewModel.State = FinanceDataState(viewModel);

            var errors = new Hashtable();
            Dictionary<string, string[]> modelStateErrors = ModelState.ToDictionary(
                modelState => modelState.Key.UnCapitalize(),
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            foreach (var error in modelStateErrors)
            {
                string[] split = error.Key.Split('.');

                if (split.Length == 1)
                {
                    errors.Add(error.Key, error.Value);
                }
                else
                {
                    if (!errors.ContainsKey(split[0]))
                    {
                        errors.Add(split[0], new Hashtable());
                    }

                    ((Hashtable)errors[split[0]]).Add(split[1].UnCapitalize(), error.Value);
                }
            }

            return new OkObjectResult(new
            {
                errors,
                data = viewModel
            });
        }

        private string FinanceDataState(FinanceDataViewModel financeData)
        {
            System.ComponentModel.DataAnnotations.ValidationContext context = new System.ComponentModel.DataAnnotations.ValidationContext(financeData);
            List<ValidationResult> result = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(financeData, context, result, true);

            if (!financeData.UpdatedAt.HasValue)
            {
                return "empty";
            }

            if (valid)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }

    }
}
