using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
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
