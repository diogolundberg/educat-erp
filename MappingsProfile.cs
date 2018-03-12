using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding 
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Enrollment, EnrollmentViewModel>().ReverseMap();
        }
    }
}