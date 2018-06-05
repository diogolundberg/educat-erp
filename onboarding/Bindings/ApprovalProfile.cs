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

            CreateMap<EnrollmentStep, Record>()
                .ForMember(x => x.SentAt, config => config.MapFrom(x => x.Enrollment.SentAt.Format()))
                .ForMember(x => x.Name, config => config.MapFrom(x => x.Enrollment.PersonalData.RealName))
                .ForMember(x => x.Email, config => config.MapFrom(x => x.Enrollment.PersonalData.Email))
                .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.Pendencies));

            CreateMap<Form, EnrollmentStep>()
                .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.Pendencies));
        }
    }
}
