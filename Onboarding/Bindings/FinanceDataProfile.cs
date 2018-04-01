using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModels;

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
