using System.Collections.Generic;

namespace Onboarding.ViewModels.FinanceApprovals
{
    public class Record
    {
        public string EnrollmentNumber { get; set; }
        public IEnumerable<FinancePendency> Pendencies { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string AssumedName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Landline { get; set; }

        public Plan Plan { get; set; }
        public string PaymentMethod { get; set; }
        public Representative Representative { get; set; }
        public ICollection<Guarantor> Guarantors { get; set; }
        public string State { get; set; }
    }
}
