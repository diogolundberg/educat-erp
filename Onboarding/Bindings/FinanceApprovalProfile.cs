using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModels.FinanceApprovals;
using System.Linq;

namespace Onboarding.Bindings
{
    public class FinanceApprovalProfile : Profile
    {
        public FinanceApprovalProfile()
        {
            CreateMap<Enrollment, Records>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.BirthDate, config => config.MapFrom(x => x.PersonalData.BirthDate.Value.ToString("dd/MM/yyyy")))
            .ForMember(x => x.Email, config => config.MapFrom(x => x.PersonalData.Email))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber))
            .ForMember(x => x.UpdatedAt, config => config.MapFrom(x => x.FinanceData.UpdatedAt))
            .ForMember(x => x.Plan, config => config.MapFrom(x => x.FinanceData.Plan.Name))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber));

            CreateMap<Enrollment, Record>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.Pendencies.OfType<Models.FinancePendency>().Select(o => new ViewModels.FinanceApprovals.FinancePendency
            {
                Description = o.Description,
                Id = o.Id,
                SectionId = o.SectionId
            })));
        }
    }
}
