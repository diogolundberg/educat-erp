using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentViewModel>().ReverseMap();
        }
    }
}
