using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
{
    public class RepresentativeProfile : Profile
    {
        public RepresentativeProfile()
        {
            CreateMap<RepresentativeCompany, RepresentativeCompanyPatchViewModel>();
            CreateMap<RepresentativeCompanyPatchViewModel, RepresentativeCompany>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<RepresentativePerson, RepresentativePersonPatchViewModel>();
            CreateMap<RepresentativePersonPatchViewModel, RepresentativeCompany>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<Representative, RepresentativePatchViewModel>()
            .Include<RepresentativeCompany, RepresentativeCompanyPatchViewModel>()
            .Include<RepresentativePerson, RepresentativePersonPatchViewModel>();
            CreateMap<RepresentativePatchViewModel, Representative>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.FinanceData, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());
        }
    }
}
