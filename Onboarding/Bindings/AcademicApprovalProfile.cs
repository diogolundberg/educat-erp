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
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF));

            CreateMap<Enrollment, Record>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.Pendencies.OfType<Models.AcademicPendency>().Select(o => new ViewModels.AcademicApprovals.AcademicPendency
            {
                Description = o.Description,
                Id = o.Id,
                SectionId = o.SectionId
            })));
        }
    }
}
