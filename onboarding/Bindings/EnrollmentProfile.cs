using AutoMapper;
using onboarding.Models;
using onboarding.Resolvers;
using onboarding.Statuses;
using onboarding.Validations;
using onboarding.Validations.FinanceData;
using onboarding.Validations.PersonalData;
using onboarding.ViewModels.Enrollments;
using System;
using System.Collections.Generic;

namespace onboarding.Bindings
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, Record>()
                .ForMember(x => x.OnboardingYear, config => config.MapFrom(x => x.Onboarding.Year))
                .ForMember(x => x.Photo, config => config.MapFrom(x => x.Photo))
                .ForMember(x => x.DaysRemaining, config => config.MapFrom(x => DateTime.Parse(x.Onboarding.EndAt).Subtract(DateTime.Now).Days))
                .ForMember(x => x.Steps, config => config.ResolveUsing<StepResolver>());
        }
    }
}