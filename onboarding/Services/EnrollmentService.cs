using System.Linq;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;

namespace onboarding.Services
{
    public class EnrollmentService : BaseService<Enrollment>
    {
        public EnrollmentService(DatabaseContext context) : base(context)
        {
        }

        public override IQueryable<Enrollment> List()
        {
            return base.List()
                        .Include("Onboarding")
                        .Include("Payment")
                        .Include("Contract")
                        .Include("EnrollmentSteps")
                        .Include("EnrollmentSteps.Pendencies")
                        .Include("EnrollmentSteps.Pendencies.Section")
                        .Include("PersonalData")
                        .Include("PersonalData.PersonalDataDisabilities")
                        .Include("PersonalData.PersonalDataSpecialNeeds")
                        .Include("PersonalData.PersonalDataDocuments")
                        .Include("PersonalData.PersonalDataDocuments.Document")
                        .Include("PersonalData.BirthCountry")
                        .Include("FinanceData")
                        .Include("FinanceData.Plan")
                        .Include("FinanceData.Representative")
                        .Include("FinanceData.Representative.AddressKind")
                        .Include("FinanceData.Representative.City")
                        .Include("FinanceData.Representative.State")
                        .Include("FinanceData.Guarantors")
                        .Include("FinanceData.Guarantors.Relationship")
                        .Include("FinanceData.Guarantors.GuarantorDocuments")
                        .Include("FinanceData.Guarantors.GuarantorDocuments.Document");
        }
    }
}
