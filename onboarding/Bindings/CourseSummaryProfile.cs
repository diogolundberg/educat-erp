using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.CourseSummaries;

namespace onboarding.Bindings
{
    public class CourseSummaryProfile : Profile
    {
        public CourseSummaryProfile()
        {
            CreateMap<Enrollment, Record>();
        }
    }
}
