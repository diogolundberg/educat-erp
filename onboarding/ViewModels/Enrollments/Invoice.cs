using onboarding.Models;

namespace onboarding.ViewModels.Enrollments
{
    public class Invoice : BaseModel
    {
        public string InvoiceNumber { get; set; }
        public string Value { get; set; }
        public string DueDate { get; set; }
        public string Billet { get; set; }
        public bool Compensated { get; set; }
        public string Status { get; set; }
    }
}
