using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
{
    public class FinanceDataProfile : Profile
    {
        public FinanceDataProfile()
        {
            CreateMap<FinanceData, FinanceDataViewModel>()
            .ForMember(x => x.Representative, config => config.MapFrom(x => x.Representative));
            CreateMap<FinanceDataViewModel, FinanceData>()
            .ForMember(x => x.Representative, config => config.MapFrom(x => x.Representative));

            CreateMap<FinanceData, FinanceDataPatchViewModel>()
            .ForMember(x => x.Representative, config => config.MapFrom(x => x.Representative));
            CreateMap<FinanceDataPatchViewModel, FinanceData>()
            .ForMember(x => x.Representative, config => config.MapFrom(x => x.Representative));
        }
    }
}
