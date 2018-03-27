using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
{
    public class GuarantorProfile : Profile
    {
        public GuarantorProfile()
        {
            CreateMap<GuarantorPatchViewModel, Guarantor>()
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore());

            CreateMap<Guarantor, GuarantorPatchViewModel>();
        }
    }
}
