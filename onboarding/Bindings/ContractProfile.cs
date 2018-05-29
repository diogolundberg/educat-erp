using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.Contracts;

namespace onboarding.Bindings
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, Record>()
            .ForMember(x => x.AcceptedAt, config => config.MapFrom(x => x.AcceptedAt.Format()));
        }
    }
}
