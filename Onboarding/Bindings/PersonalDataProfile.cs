using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;
using System.Linq;

namespace Onboarding.Bindings
{
    public class PersonalDataProfile : Profile
    {
        public PersonalDataProfile()
        {
            CreateMap<PersonalData, PersonalDataViewModel>()
            .ForMember(x => x.Disabilities, config => config.MapFrom(x => x.PersonalDataDisabilities.Select(o => o.DisabilityId)))
            .ForMember(x => x.SpecialNeeds, config => config.MapFrom(x => x.PersonalDataSpecialNeeds.Select(o => o.SpecialNeedId)))
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.PersonalDataDocuments.Select(o => new DocumentViewModel
            {
                Id = o.Document.Id,
                Url = o.Document.Url,
                DocumentTypeId = o.Document.DocumentTypeId
            })));

            CreateMap<PersonalDataViewModel, PersonalData>()
            .ForMember(x => x.MaritalStatus, config => config.Ignore())
            .ForMember(x => x.Gender, config => config.Ignore())
            .ForMember(x => x.BirthState, config => config.Ignore())
            .ForMember(x => x.BirthCountry, config => config.Ignore())
            .ForMember(x => x.City, config => config.Ignore())
            .ForMember(x => x.BirthCity, config => config.Ignore())
            .ForMember(x => x.HighSchoolGraduationCountry, config => config.Ignore())
            .ForMember(x => x.State, config => config.Ignore())
            .ForMember(x => x.AddressKind, config => config.Ignore())
            .ForMember(x => x.Race, config => config.Ignore())
            .ForMember(x => x.HighSchollKind, config => config.Ignore())
            .ForMember(x => x.Enrollment, config => config.Ignore())
            .ForMember(x => x.PersonalDataDisabilities, config => config.MapFrom(cm => cm.Disabilities.Select(x => new PersonalDataDisability
            {
                DisabilityId = x
            })))
            .ForMember(x => x.PersonalDataSpecialNeeds, config => config.MapFrom(cm => cm.SpecialNeeds.Select(x => new PersonalDataSpecialNeed
            {
                SpecialNeedId = x
            })))
            .ForMember(x => x.PersonalDataDocuments, config => config.MapFrom(cm => cm.Documents.Select(x => new PersonalDataDocument
            {
                Document = new Document
                {
                    Id = x.Id ?? 0,
                    Url = x.Url,
                    DocumentTypeId = x.DocumentTypeId
                }
            })));
        }
    }
}
