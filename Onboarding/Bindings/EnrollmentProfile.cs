using AutoMapper;
using Onboarding.Statuses;
using Onboarding.ViewModels.Enrollments;
using System;
using System.Linq;

namespace Onboarding.Bindings
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Models.Enrollment, Record>()
                .ForMember(x => x.OnboardingYear, config => config.MapFrom(x => x.Onboarding.Year))
                .ForMember(x => x.StartedAt, config => config.MapFrom(x => x.StartedAt))
                .ForMember(x => x.FirstTime, config => config.MapFrom(x => !x.StartedAt.HasValue))
                .ForMember(x => x.Deadline, config => config.MapFrom(x => x.Onboarding.EndAt))
                .ForMember(x => x.Photo, config => config.MapFrom(x => x.Photo))
                .ForMember(x => x.PersonalData, config => config.MapFrom(x => x.PersonalData))
                .ForMember(x => x.FinanceData, config => config.MapFrom(x => x.FinanceData))
                .ForMember(x => x.DaysRemaining, config => config.MapFrom(x => DateTime.Parse(x.Onboarding.EndAt).Subtract(DateTime.Now).Days))
                .ForMember(x => x.AcademicApproval, config => config.MapFrom(x => new
                {
                    status = (new AcademicApprovalStatus(null, x)).GetStatus(),
                    pendencies = x.AcademicPendencies.Select(ap => new { ap.Description, ap.SectionId, ap.Section.Name })
                }))
                .ForMember(x => x.FinanceApproval, config => config.MapFrom(x => new
                {
                    status = (new FinanceApprovalStatus(null, x)).GetStatus(),
                    pendencies = x.FinancePendencies.Select(fp => new { fp.Description, fp.SectionId, fp.Section.Name }),
                }));
        }
    }
}
