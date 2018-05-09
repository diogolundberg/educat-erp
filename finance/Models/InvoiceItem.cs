using System.ComponentModel.DataAnnotations.Schema;

namespace finance.Models
{
    public class InvoiceItem : BaseModel
    {
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public string EnrollmentNumber { get; set; }
    }
}
