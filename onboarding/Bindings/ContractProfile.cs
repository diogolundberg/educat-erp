using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.Contracts;
using System;

namespace onboarding.Bindings
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, Record>()
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.EnrollmentStep.Pendencies))
            .ForMember(x => x.AcceptedAt, config => config.MapFrom(x => x.AcceptedAt.Format()))
            .ForMember(x => x.Accept, config => config.MapFrom(x => x.CreatedAt.HasValue));

            CreateMap<Form, Contract>()
                .ForMember(x => x.AcceptedAt, config => config.MapFrom(x => x.Accept ? (DateTime?)DateTime.Now : null));
        }
    }
}
