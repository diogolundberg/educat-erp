using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class GuarantorDocument
    {
        [ForeignKey("Guarantor")]
        public int GuarantorId { get; set; }

        public virtual Guarantor Guarantor { get; set; }

        [ForeignKey("Document")]
        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }
    }
}
