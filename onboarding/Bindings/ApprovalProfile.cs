using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.Approvals;

namespace onboarding.Bindings
{
    public class ApprovalProfile : Profile
    {
        public ApprovalProfile()
        {
            CreateMap<EnrollmentStep, Records>()
                .ForMember(x => x.SentAt, config => config.MapFrom(x => x.Enrollment.SentAt.Format()))
                .ForMember(x => x.Name, config => config.MapFrom(x => x.Enrollment.PersonalData.RealName))
                .ForMember(x => x.Email, config => config.MapFrom(x => x.Enrollment.PersonalData.Email));
        }
    }
}
