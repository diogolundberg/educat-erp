using System.Collections.Generic;

namespace Onboarding.ViewModels.FinanceApprovals
{
    public class Form
    {
        public Form()
        {
            Pendencies = new HashSet<FinancePendency>();
        }

        public string EnrollmentNumber { get; set; }

        public IEnumerable<FinancePendency> Pendencies { get; set; }
    }
}
