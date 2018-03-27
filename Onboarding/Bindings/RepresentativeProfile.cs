using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
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
            CreateMap<RepresentativePersonViewModel, RepresentativeCompany>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<Representative, RepresentativeViewModel>()
            .Include<RepresentativeCompany, RepresentativeCompanyViewModel>()
            .Include<RepresentativePerson, RepresentativePersonViewModel>();
            CreateMap<RepresentativeViewModel, Representative>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.FinanceData, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());
        }
    }
}
