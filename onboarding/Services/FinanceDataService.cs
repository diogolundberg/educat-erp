﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;
using onboarding.ViewModels;
using onboarding.ViewModels.FinanceDatas;
using System.Linq;

namespace onboarding.Services
{
    public class FinanceDataService : BaseService<FinanceData>
    {
        private readonly IMapper _mapper;

        public FinanceDataService(DatabaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override IQueryable<FinanceData> List()
        {
            return base.List()
                        .Include("Enrollment.Onboarding")
                        .Include("Enrollment")
                        .Include("Enrollment.Pendencies")
                        .Include("Enrollment.PersonalData")
                        .Include("Representative")
                        .Include("Guarantors")
                        .Include("Guarantors.Relationship")
                        .Include("Guarantors.GuarantorDocuments")
                        .Include("Guarantors.GuarantorDocuments.Document");
        }

        public FinanceData Update(Form obj, FinanceData financeData)
        {
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

            return base.Update(financeData);
        }
    }
}
