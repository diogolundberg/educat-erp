﻿using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels;
using onboarding.ViewModels.FinanceDatas;
using System.Linq;

namespace onboarding.Bindings
{
    public class GuarantorProfile : Profile
    {
        public GuarantorProfile()
        {
            CreateMap<GuarantorViewModel, Guarantor>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore())
            .ForMember(x => x.Relationship, config => config.Ignore())
            .ForMember(x => x.GuarantorDocuments, config => config.MapFrom(cm => cm.Documents.Select(x => new GuarantorDocument
            {
                Document = new Document
                {
                    Id = x.Id ?? 0,
                    Url = x.Url,
                    DocumentTypeId = x.DocumentTypeId
                }
            })));

            CreateMap<Guarantor, GuarantorViewModel>()
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.GuarantorDocuments.Select(o => new DocumentViewModel
            {
                Id = o.Document.Id,
                Url = o.Document.Url,
                DocumentTypeId = o.Document.DocumentTypeId
            })));
        }
    }
}
