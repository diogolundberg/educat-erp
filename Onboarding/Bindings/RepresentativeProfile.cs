using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels;

namespace onboarding.Bindings
{
    public class RepresentativeProfile : Profile
    {
        public RepresentativeProfile()
        {
            CreateMap<RepresentativeCompany, RepresentativeCompanyViewModel>();
            CreateMap<RepresentativeCompanyViewModel, RepresentativeCompany>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<RepresentativePerson, RepresentativePersonViewModel>();
            CreateMap<RepresentativePersonViewModel, RepresentativePerson>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<Representative, RepresentativeViewModel>()
            .ForMember(x => x.Discriminator, config => config.MapFrom(x => x is RepresentativePerson ? "RepresentativePerson" : "RepresentativeCompany"))
            .Include<RepresentativeCompany, RepresentativeCompanyViewModel>()
            .Include<RepresentativePerson, RepresentativePersonViewModel>();

            CreateMap<RepresentativeViewModel, Representative>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.FinanceData, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore())
            .ForMember(x => x.AddressKind, config => config.Ignore())
            .Include<RepresentativeCompanyViewModel, RepresentativeCompany>()
            .Include<RepresentativePersonViewModel, RepresentativePerson>();
        }
    }
}
