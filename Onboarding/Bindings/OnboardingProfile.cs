using AutoMapper;
using System.Linq;

namespace Onboarding.Bindings
{
    public class OnboardingProfile : Profile
    {
        public OnboardingProfile()
        {
            CreateMap<ViewModels.Onboarding.Form, Models.Onboarding>()
            .ForMember(x => x.Enrollments, config => config.MapFrom(x => x.Enrollments.Select(o => new Models.Enrollment
            {
                PersonalData = new Models.PersonalData
                {
                    RealName = o.Name,
                    CPF = o.CPF,
                    Email = o.Email
                }
            })));

            CreateMap<Models.Onboarding, ViewModels.Onboarding.Records>()
                .ForMember(x => x.EnrollmentCount, config => config.MapFrom(x => x.Enrollments.Count));
        }
    }
}
