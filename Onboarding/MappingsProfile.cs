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

            CreateMap<FinanceData, FinanceDataViewModel>();
            CreateMap<FinanceDataViewModel, FinanceData>();

            CreateMap<PersonalData, PersonalDataViewModel>()
                .ForMember(x => x.Disabilities, config => config.MapFrom(x => x.PersonalDataDisabilities.Select(o => o.DisabilityId)))
                .ForMember(x => x.SpecialNeeds, config => config.MapFrom(x => x.PersonalDataSpecialNeeds.Select(o => o.SpecialNeedId)))
                .ForMember(x => x.Documents, config => config.MapFrom(x => x.PersonalDataDocuments.Select(o => new DocumentViewModel
                {
                    ExternalId = o.Document.ExternalId,
                    DocumentTypeId = o.Document.DocumentTypeId
                })));

            CreateMap<PersonalDataViewModel, PersonalData>()
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
                                                .MapFrom(cm => cm.Disabilities.Select(x => new PersonalDataDisability { DisabilityId = x })))
                .ForMember(x => x.PersonalDataSpecialNeeds, config => config
                                                .MapFrom(cm => cm.SpecialNeeds.Select(x => new PersonalDataSpecialNeed { SpecialNeedId = x })))
                .ForMember(x => x.PersonalDataDocuments, config => config.MapFrom(cm => cm.Documents.Select(x => new PersonalDataDocument
                {
                    Document = new Document
                    {
                        ExternalId = x.ExternalId,
                        DocumentTypeId = x.DocumentTypeId
                    }
                })));

        }
    }
}