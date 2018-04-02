using System.Collections.Generic;

namespace Onboarding.ViewModels.FinanceApprovals
{
    public class Record
    {
        public string Name { get; set; }

        public string CPF { get; set; }

        public string EnrollmentNumber { get; set; }

        public IEnumerable<FinancePendency> Pendencies { get; set; }
    }
}
