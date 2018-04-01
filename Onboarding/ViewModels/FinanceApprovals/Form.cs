using System.Collections.Generic;

namespace Onboarding.ViewModels.FinanceApprovals
{
    public class Form
    {
        public Form()
        {
            FinancePendencies = new HashSet<FinancePendency>();
        }

        public string EnrollmentNumber { get; set; }

        public IEnumerable<FinancePendency> FinancePendencies { get; set; }
    }
}
