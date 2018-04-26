using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels;

namespace onboarding.Bindings
{
    public class FinanceDataProfile : Profile
    {
        public FinanceDataProfile()
        {
            CreateMap<FinanceData, FinanceDataViewModel>();
            CreateMap<FinanceDataViewModel, FinanceData>();
        }
    }
}
