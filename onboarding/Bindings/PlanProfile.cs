using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.FinanceDatas;

namespace onboarding.Bindings
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<PlanViewModel, Plan>().ReverseMap();
        }
    }
}
