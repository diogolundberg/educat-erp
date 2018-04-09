using AutoMapper;
using Onboarding.ViewModels.FinanceApprovals;
using System.Linq;

namespace Onboarding.Bindings
{
    public class FinanceApprovalProfile : Profile
    {
        public FinanceApprovalProfile()
        {
            CreateMap<Models.Enrollment, Records>()
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.BirthDate, config => config.MapFrom(x => x.PersonalData.BirthDate.Value.ToString("dd/MM/yyyy")))
            .ForMember(x => x.Email, config => config.MapFrom(x => x.PersonalData.Email))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber))
            .ForMember(x => x.UpdatedAt, config => config.MapFrom(x => x.FinanceData.UpdatedAt))
            .ForMember(x => x.Plan, config => config.MapFrom(x => x.FinanceData.Plan.Name))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber));

            CreateMap<Models.Guarantor, Guarantor>()
            .ForMember(x => x.AddressKind, config => config.MapFrom(x => x.AddressKind.Name))
            .ForMember(x => x.City, config => config.MapFrom(x => x.City.Name))
            .ForMember(x => x.State, config => config.MapFrom(x => x.State.Name))
            .ForMember(x => x.Relationship, config => config.MapFrom(x => x.Relationship.Name))
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.GuarantorDocuments.Select(o => new
            {
                Name = o.Document.DocumentType.Name,
                Url = o.Document.Url
            })));

            CreateMap<Models.RepresentativeCompany, RepresentativeCompany>()
            .ForMember(x => x.AddressKind, config => config.MapFrom(x => x.AddressKind.Name))
            .ForMember(x => x.City, config => config.MapFrom(x => x.City.Name))
            .ForMember(x => x.State, config => config.MapFrom(x => x.State.Name));

            CreateMap<Models.RepresentativePerson, RepresentativePerson>()
            .ForMember(x => x.AddressKind, config => config.MapFrom(x => x.AddressKind.Name))
            .ForMember(x => x.City, config => config.MapFrom(x => x.City.Name))
            .ForMember(x => x.State, config => config.MapFrom(x => x.State.Name));

            CreateMap<Models.Representative, Representative>()
            .ForMember(x => x.Discriminator, config => config.MapFrom(x => x is Models.RepresentativePerson ? "RepresentativePerson" : "RepresentativeCompany"))
            .Include<Models.RepresentativeCompany, RepresentativeCompany>()
            .Include<Models.RepresentativePerson, RepresentativePerson>();

            CreateMap<Models.Plan, Plan>()
            .ForMember(x => x.DueDate, config => config.MapFrom(x => x.DueDate != null ? x.DueDate.Value.ToString("dd/MM/yyyy") : null));

            CreateMap<Models.Guarantor, Guarantor>()
            .ForMember(x => x.AddressKind, config => config.MapFrom(x => x.AddressKind.Name))
            .ForMember(x => x.City, config => config.MapFrom(x => x.City.Name))
            .ForMember(x => x.State, config => config.MapFrom(x => x.State.Name))
            .ForMember(x => x.Relationship, config => config.MapFrom(x => x.Relationship.Name))
            .ForMember(x => x.Documents, config => config.MapFrom(x => x.GuarantorDocuments.Select(o => new
            {
                Name = o.Document.DocumentType.Name,
                Url = o.Document.Url
            })));

            CreateMap<Models.Enrollment, Record>()
            .ForMember(x => x.EnrollmentNumber, config => config.MapFrom(x => x.ExternalId))
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.Pendencies.OfType<Models.FinancePendency>().Select(o => new ViewModels.FinanceApprovals.FinancePendency
            {
                Description = o.Description,
                Id = o.Id,
                SectionId = o.SectionId
            })))
            .ForMember(x => x.Name, config => config.MapFrom(x => x.PersonalData.RealName))
            .ForMember(x => x.AssumedName, config => config.MapFrom(x => x.PersonalData.AssumedName))
            .ForMember(x => x.CPF, config => config.MapFrom(x => x.PersonalData.CPF))
            .ForMember(x => x.Email, config => config.MapFrom(x => x.PersonalData.Email))
            .ForMember(x => x.PhoneNumber, config => config.MapFrom(x => x.PersonalData.PhoneNumber))
            .ForMember(x => x.Landline, config => config.MapFrom(x => x.PersonalData.Landline))
            .ForMember(x => x.Plan, config => config.MapFrom(x => x.FinanceData.Plan))
            .ForMember(x => x.PaymentMethod, config => config.MapFrom(x => x.FinanceData.PaymentMethod.Name));
        }
    }
}
