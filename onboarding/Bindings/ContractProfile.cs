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
            .ForMember(x => x.AcceptedAt, config => config.MapFrom(x => x.AcceptedAt.Format()));

            CreateMap<Form, Contract>()
                .ForMember(x => x.AcceptedAt, config => config.MapFrom(x => x.Accept ? (DateTime?)DateTime.Now : null));
        }
    }
}
