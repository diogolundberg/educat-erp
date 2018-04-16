using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Models;
using Onboarding.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Onboarding.Validations;
using Onboarding.Validations.FinanceData;
using Onboarding.Statuses;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FinanceDataController : BaseController
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
            FinanceData financeData = _context.Set<FinanceData>()
                                              .Include("Enrollment.Onboarding")
                                              .Include("Enrollment")
                                              .Include("Enrollment.Pendencies")
                                              .Include("Enrollment.PersonalData")
                                              .Include("Representative")
                                              .Include("Guarantors")
                                              .Include("Guarantors.Relationship")
                                              .Include("Guarantors.GuarantorDocuments")
                                              .Include("Guarantors.GuarantorDocuments.Document")
                                              .SingleOrDefault(x => x.Enrollment.ExternalId == token);

            if (financeData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Link para matrícula inválido." } });
            }

            if (!financeData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "O prazo para esta matrícula foi encerrado." } });
            }

            if (!string.IsNullOrEmpty(financeData.Enrollment.SentAt))
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Estes dados já foram enviados para a análises e não pode ser editados no momento." } });
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
            else if (obj.Representative is RepresentativeCompanyViewModel)
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
                            .Any(c => c.Id == guarantorDocument.DocumentId))
                        {
                            _context.Set<GuarantorDocument>().Remove(guarantorDocument);
                            _context.Set<Document>().Remove(_context.Set<Document>().Find(guarantorDocument.DocumentId));
                        }
                    }
                    foreach (DocumentViewModel guarantorDocumentViewModel in guarantorViewModel.Documents)
                    {
                        GuarantorDocument existingGuarantorDocument = existingGuarantor.GuarantorDocuments
                            .Where(c => c.DocumentId == guarantorDocumentViewModel.Id)
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

            _context.FinanceDatas.Update(financeData);

            _context.SaveChanges();
            _context.Entry(financeData).Reload();
            _context.Entry(financeData).Collection(x => x.Guarantors).Load();

            FinanceDataViewModel viewModel = _mapper.Map<FinanceDataViewModel>(financeData);
            viewModel.Status = (new FinanceDataStatus(new FinanceDataValidator(_context), financeData, new FinanceDataMessagesValidator(_context))).GetStatus();

            FinanceDataValidator validator = new FinanceDataValidator(_context);
            FluentValidation.Results.ValidationResult results = validator.Validate(financeData);
            Hashtable errors = FormatErrors(results);

            FinanceDataMessagesValidator messagesValidator = new FinanceDataMessagesValidator(_context);
            List<string> messages = messagesValidator.Validate(financeData).Errors.Select(x => x.ErrorMessage).Distinct().ToList();

            return new OkObjectResult(new
            {
                messages,
                errors,
                data = viewModel
            });
        }
    }
}
