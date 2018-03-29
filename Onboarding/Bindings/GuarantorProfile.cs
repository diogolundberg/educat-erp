﻿using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
{
    public class GuarantorProfile : Profile
    {
        public GuarantorProfile()
        {
            CreateMap<GuarantorViewModel, Guarantor>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<Guarantor, GuarantorViewModel>()
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.PersonalDataDocuments.Select(o => new DocumentViewModel
            {
                Id = o.Document.Id,
                Url = o.Document.Url,
                DocumentTypeId = o.Document.DocumentTypeId
            })));
        }
    }
}
