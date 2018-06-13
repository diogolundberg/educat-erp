using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class Payment : BaseModel
    {
        [ForeignKey("Enrollment")]
        public int? EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }

        public string BilletUrl { get; set; }
        public string Url { get; set; }
        public int? InvoiceNumber { get; set; }

        [ForeignKey("EnrollmentStep")]
        public int? EnrollmentStepId { get; set; }

        public virtual EnrollmentStep EnrollmentStep { get; set; }
    }
}
