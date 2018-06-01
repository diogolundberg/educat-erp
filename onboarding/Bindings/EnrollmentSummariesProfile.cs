using AutoMapper;
using onboarding.ViewModels.EnrollmentSummaries;
using onboarding.Models;
using System.Linq;

namespace onboarding.Bindings
{
    public class EnrollmentSummariesProfile : Profile
    {
        public EnrollmentSummariesProfile()
        {
            CreateMap<Enrollment, Record>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.AssumedName, config => config.MapFrom(x => x.PersonalData.AssumedName))
            .ForMember(x => x.Cpf, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.Email, config => config.MapFrom(x => x.PersonalData.Email))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber))
            .ForMember(x => x.Landline, config => config.MapFrom(x => x.PersonalData.Landline))
            .ForMember(x => x.BirthDate, config => config.MapFrom(x => x.PersonalData.BirthDate))
            .ForMember(x => x.HighSchoolGraduationYear, config => config.MapFrom(x => x.PersonalData.HighSchoolGraduationYear))
            .ForMember(x => x.Zipcode, config => config.MapFrom(x => x.PersonalData.Zipcode))
            .ForMember(x => x.StreetAddress, config => config.MapFrom(x => x.PersonalData.StreetAddress))
            .ForMember(x => x.AddressNumber, config => config.MapFrom(x => x.PersonalData.AddressNumber))
            .ForMember(x => x.ComplementAddress, config => config.MapFrom(x => x.PersonalData.ComplementAddress))
            .ForMember(x => x.Neighborhood, config => config.MapFrom(x => x.PersonalData.Neighborhood))
            .ForMember(x => x.MothersName, config => config.MapFrom(x => x.PersonalData.MothersName))
            .ForMember(x => x.Handicap, config => config.MapFrom(x => x.PersonalData.Handicap))
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
            .ForMember(x => x.HighSchoolKind, config => config.MapFrom(x => x.PersonalData.HighSchoolKind.Name))
            .ForMember(x => x.Nationality, config => config.MapFrom(x => x.PersonalData.Nationality.Name))
            .ForMember(x => x.SpecialNeeds, config => config.MapFrom(x => x.PersonalData.PersonalDataSpecialNeeds.Select(o => o.SpecialNeed.Name)))
            .ForMember(x => x.Disabilities, config => config.MapFrom(x => x.PersonalData.PersonalDataDisabilities.Select(o => o.Disability.Name)))
            .ForMember(x => x.Plan, config => config.MapFrom(x => x.FinanceData.Plan))
            .ForMember(x => x.Representative, config => config.MapFrom(x => x.FinanceData.Representative))
            .ForMember(x => x.Guarantors, config => config.MapFrom(x => x.FinanceData.Guarantors))
            .ForMember(x => x.PaymentMethod, config => config.MapFrom(x => x.FinanceData.PaymentMethod.Name))
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.PersonalData.PersonalDataDocuments.Select(o => new
            {
                o.Document.DocumentType.Name,
                o.Document.Url
            })));
        }
    }
}
