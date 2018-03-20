using System.Linq;
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
            CreateMap<PersonalData, PersonalViewModel>();
            CreateMap<PersonalViewModel, PersonalData>()
                .ForMember(x => x.MaritalStatus, config => config.Ignore())
                .ForMember(x => x.Gender, config => config.Ignore())
                .ForMember(x => x.BirthState, config => config.Ignore())
                .ForMember(x => x.BirthCountry, config => config.Ignore())
                .ForMember(x => x.HighSchoolGraduationCountry, config => config.Ignore())
                .ForMember(x => x.State, config => config.Ignore())
                .ForMember(x => x.AddressKind, config => config.Ignore())
                .ForMember(x => x.Race, config => config.Ignore())
                .ForMember(x => x.HighSchollKind, config => config.Ignore())
                .ForMember(x => x.Enrollment, config => config.Ignore())
                .ForMember(x => x.PersonalDataDisabilities, config => config
                                                .MapFrom(cm => cm.Deficiencies.Select(x => new PersonalDataDisability { DisabilityId = x })))
                .ForMember(x => x.PersonalDataSpecialNeeds, config => config
                                                .MapFrom(cm => cm.SpecialNeeds.Select(x => new PersonalDataSpecialNeed { SpecialNeedId = x })));

        }
    }
}