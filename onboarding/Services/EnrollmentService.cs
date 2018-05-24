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
                        .Include("Pendencies")
                        .Include("Pendencies.Section")
                        .Include("PersonalData")
                        .Include("PersonalData.PersonalDataDisabilities")
                        .Include("PersonalData.PersonalDataSpecialNeeds")
                        .Include("PersonalData.PersonalDataDocuments")
                        .Include("PersonalData.PersonalDataDocuments.Document")
                        .Include("PersonalData.BirthCountry")
                        .Include("FinanceData")
                        .Include("FinanceData.Plan")
                        .Include("FinanceData.Representative")
                        .Include("FinanceData.Guarantors")
                        .Include("FinanceData.Guarantors.Relationship")
                        .Include("FinanceData.Guarantors.GuarantorDocuments")
                        .Include("FinanceData.Guarantors.GuarantorDocuments.Document");
        }
    }
}
