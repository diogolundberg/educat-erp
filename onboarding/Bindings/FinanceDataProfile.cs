using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.FinanceDatas;

namespace onboarding.Bindings
{
    public class FinanceDataProfile : Profile
    {
        public FinanceDataProfile()
        {
            CreateMap<FinanceData, Record>();
            CreateMap<Form, FinanceData>();
        }
    }
}
