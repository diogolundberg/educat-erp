using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding 
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Enrollment, EnrollmentViewModel>();
        }
    }
}