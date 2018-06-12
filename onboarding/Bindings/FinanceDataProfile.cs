using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.FinanceDatas;

namespace onboarding.Bindings
{
    public class FinanceDataProfile : Profile
    {
        public FinanceDataProfile()
        {
            CreateMap<FinanceData, Record>()
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.EnrollmentStep.Pendencies));

            CreateMap<Form, FinanceData>();
        }
    }
}
