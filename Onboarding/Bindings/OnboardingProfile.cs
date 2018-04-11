using AutoMapper;
using Onboarding.ViewModels.Onboarding;
using System;
using System.Globalization;
using System.Linq;

namespace Onboarding.Bindings
{
    public class OnboardingProfile : Profile
    {
        public OnboardingProfile()
        {
            CreateMap<ViewModels.Onboarding.Form, Models.Onboarding>()
            .ForMember(x => x.StartAt, config => config.MapFrom(x => x.StartAt))
            .ForMember(x => x.EndAt, config => config.MapFrom(x => x.EndAt))
            .ForMember(x => x.Enrollments, config => config.MapFrom(x => x.Enrollments.Select(o => new Models.Enrollment
            {
                Id = o.Id,
                PersonalData = new Models.PersonalData
                {
                    RealName = o.Name,
                    CPF = o.CPF,
                    Email = o.Email
                }
            })));

            CreateMap<Models.Onboarding, ViewModels.Onboarding.Form>()
            .ForMember(x => x.StartAt, config => config.MapFrom(x => x.StartAt))
            .ForMember(x => x.EndAt, config => config.MapFrom(x => x.EndAt))
            .ForMember(x => x.Enrollments, config => config.MapFrom(x => x.Enrollments.Select(o => new EnrollmentForm
            {
                Id = o.Id,
                Name = o.PersonalData.RealName,
                CPF = o.PersonalData.CPF,
                Email = o.PersonalData.Email,
            })));

            CreateMap<Models.Onboarding, ViewModels.Onboarding.Records>()
                .ForMember(x => x.EnrollmentCount, config => config.MapFrom(x => x.Enrollments.Count));
        }
    }
}
