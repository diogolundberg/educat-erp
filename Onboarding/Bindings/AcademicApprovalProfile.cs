using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModels;

namespace Onboarding.Bindings
{
    public class AcademicApprovalProfile : Profile
    {
        public AcademicApprovalProfile()
        {
            CreateMap<Enrollment, AcademicApprovalViewModel>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF));
        }
    }
}
