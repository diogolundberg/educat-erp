using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModels.AcademicApprovals;
using System.Linq;

namespace Onboarding.Bindings
{
    public class AcademicApprovalProfile : Profile
    {
        public AcademicApprovalProfile()
        {
            CreateMap<Enrollment, Records>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.BirthDate, config => config.MapFrom(x => x.PersonalData.BirthDate))
            .ForMember(x => x.Email, config => config.MapFrom(x => x.PersonalData.Email))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber))
            .ForMember(x => x.UpdatedAt, config => config.MapFrom(x => x.PersonalData.UpdatedAt));

            CreateMap<Record, PersonalData>();

            CreateMap<PersonalData, Record>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.RealName))
            .ForMember(x => x.Photo, config => config.MapFrom(x => x.Enrollment.Photo))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.Enrollment.ExternalId))
            .ForMember(x => x.Gender, config => config.MapFrom(x => x.Gender.Name))
            .ForMember(x => x.MaritalStatus, config => config.MapFrom(x => x.MaritalStatus.Name))
            .ForMember(x => x.BirthCity, config => config.MapFrom(x => x.BirthCity.Name))
            .ForMember(x => x.BirthState, config => config.MapFrom(x => x.BirthState.Name))
            .ForMember(x => x.BirthCountry, config => config.MapFrom(x => x.BirthCountry.Name))
            .ForMember(x => x.HighSchoolGraduationCountry, config => config.MapFrom(x => x.HighSchoolGraduationCountry.Name))
            .ForMember(x => x.City, config => config.MapFrom(x => x.City.Name))
            .ForMember(x => x.State, config => config.MapFrom(x => x.State.Name))
            .ForMember(x => x.AddressKind, config => config.MapFrom(x => x.AddressKind.Name))
            .ForMember(x => x.Race, config => config.MapFrom(x => x.Race.Name))
            .ForMember(x => x.HighSchollKind, config => config.MapFrom(x => x.HighSchollKind.Name))
            .ForMember(x => x.Nationality, config => config.MapFrom(x => x.Nationality.Name))
            .ForMember(x => x.SpecialNeeds, config => config.MapFrom(x => x.PersonalDataSpecialNeeds.Select(o => o.SpecialNeed.Name)))
            .ForMember(x => x.Disabilities, config => config.MapFrom(x => x.PersonalDataDisabilities.Select(o => o.Disability.Name)))
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.PersonalDataDocuments.Select(o => new
            {
                Name = o.Document.DocumentType.Name,
                Url = o.Document.Url
            })))
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.Enrollment.Pendencies.OfType<Models.AcademicPendency>().Select(o => new ViewModels.AcademicApprovals.AcademicPendency
            {
                Description = o.Description,
                Id = o.Id,
                SectionId = o.SectionId
            })));
        }
    }
}
