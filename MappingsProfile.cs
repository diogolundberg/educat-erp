using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding 
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Enrollment, EnrollmentViewModel>();
            CreateMap<EnrollmentViewModel, Enrollment>()
            .ForMember(x => x.CivilStatus, config => config.Ignore())
            .ForMember(x => x.Gender, config => config.Ignore())
            .ForMember(x => x.Nationality, config => config.Ignore())
            .ForMember(x => x.OriginCountry, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore())
            .ForMember(x => x.CountryOfGraduationFromHighSchool, config => config.Ignore())
            .ForMember(x => x.PhoneType, config => config.Ignore())
            .ForMember(x => x.CountryState, config => config.Ignore())
            .ForMember(x => x.AddressType, config => config.Ignore())
            .ForMember(x => x.Race, config => config.Ignore())
            .ForMember(x => x.School, config => config.Ignore())
            .ForMember(x => x.EnrollmentDisabilities, config => config.Ignore());
        }
    }
}