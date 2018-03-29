using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
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
