using AutoMapper;
using onboarding.ViewModels.EnrollmentSummaries;
using Onboarding.Models;
using System.Linq;

namespace Onboarding.Bindings
{
    public class EnrollmentSummariesProfile : Profile
    {
        public EnrollmentSummariesProfile()
        {
            CreateMap<Enrollment, Record>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.Photo, config => config.MapFrom(x => x.Photo))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.Gender, config => config.MapFrom(x => x.PersonalData.Gender.Name))
            .ForMember(x => x.MaritalStatus, config => config.MapFrom(x => x.PersonalData.MaritalStatus.Name))
            .ForMember(x => x.BirthCity, config => config.MapFrom(x => x.PersonalData.BirthCity.Name))
            .ForMember(x => x.BirthState, config => config.MapFrom(x => x.PersonalData.BirthState.Name))
            .ForMember(x => x.BirthCountry, config => config.MapFrom(x => x.PersonalData.BirthCountry.Name))
            .ForMember(x => x.HighSchoolGraduationCountry, config => config.MapFrom(x => x.PersonalData.HighSchoolGraduationCountry.Name))
            .ForMember(x => x.City, config => config.MapFrom(x => x.PersonalData.City.Name))
            .ForMember(x => x.State, config => config.MapFrom(x => x.PersonalData.State.Name))
            .ForMember(x => x.AddressKind, config => config.MapFrom(x => x.PersonalData.AddressKind.Name))
            .ForMember(x => x.Race, config => config.MapFrom(x => x.PersonalData.Race.Name))
            .ForMember(x => x.HighSchollKind, config => config.MapFrom(x => x.PersonalData.HighSchollKind.Name))
            .ForMember(x => x.Nationality, config => config.MapFrom(x => x.PersonalData.Nationality.Name))
            .ForMember(x => x.SpecialNeeds, config => config.MapFrom(x => x.PersonalData.PersonalDataSpecialNeeds.Select(o => o.SpecialNeed.Name)))
            .ForMember(x => x.Disabilities, config => config.MapFrom(x => x.PersonalData.PersonalDataDisabilities.Select(o => o.Disability.Name)))
            .ForMember(x => x.Plan, config => config.MapFrom(x => x.FinanceData.Plan))
            .ForMember(x => x.Representative, config => config.MapFrom(x => x.FinanceData.Representative))
            .ForMember(x => x.Guarantors, config => config.MapFrom(x => x.FinanceData.Guarantors))
            .ForMember(x => x.PaymentMethod, config => config.MapFrom(x => x.FinanceData.PaymentMethod.Name))
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.PersonalData.PersonalDataDocuments.Select(o => new
            {
                Name = o.Document.DocumentType.Name,
                Url = o.Document.Url
            })))
            .ForMember(x => x.FinancePendencies, config => config.MapFrom(x => x.Pendencies.OfType<Models.FinancePendency>().Select(o => new ViewModels.FinanceApprovals.FinancePendency
            {
                Description = o.Description,
                Id = o.Id,
                SectionId = o.SectionId,
                Anchor = o.Section.Anchor
            })))
            .ForMember(x => x.AcademicPendencies, config => config.MapFrom(x => x.Pendencies.OfType<Models.AcademicPendency>().Select(o => new ViewModels.AcademicApprovals.AcademicPendency
            {
                Description = o.Description,
                Id = o.Id,
                SectionId = o.SectionId,
                Anchor = o.Section.Anchor
            })));
        }
    }
}
