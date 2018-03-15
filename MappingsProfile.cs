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
                .ForMember(x => x.EnrollmentDisabilities, config => config.Ignore())
                .ForMember(x => x.Guarantor, config => config.MapFrom(cm => new EnrollmentPerson { 
                                                                                DocumentTypeId = cm.GuarantorDocumentTypeId,
                                                                                Cpf = cm.GuarantorCpf,
                                                                                Cnpj = cm.GuarantorCnpj,
                                                                                Name = cm.GuarantorName,
                                                                                Contact = cm.GuarantorContact,
                                                                                Address = cm.GuarantorAddress,
                                                                                Phone1 = cm.GuarantorPhone1,
                                                                                Phone2 = cm.GuarantorPhone2,
                                                                                Email = cm.GuarantorEmail
                                                                            }))
                .ForMember(x => x.Responsible, config => config.MapFrom(cm => new EnrollmentPerson {
                                                                        DocumentTypeId = cm.ResponsibleDocumentTypeId,
                                                                        Cpf = cm.ResponsibleCpf,
                                                                        Cnpj = cm.ResponsibleCnpj,
                                                                        Name = cm.ResponsibleName,
                                                                        Contact = cm.ResponsibleContact,
                                                                        Address = cm.ResponsibleAddress,
                                                                        Phone1 = cm.ResponsiblePhone1,
                                                                        Phone2 = cm.ResponsiblePhone2,
                                                                        Email = cm.ResponsibleEmail
                                                                    }))
                .ForMember(x => x.EnrollmentDisabilities, config => config
                                                        .MapFrom(cm => 
                                                                    cm.Deficiencies
                                                                    .Select(dfs => new EnrollmentDisability { DisabilityId = dfs })));
        }
    }
}