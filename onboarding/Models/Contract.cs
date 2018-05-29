using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class Contract : BaseModel
    {
        [ForeignKey("Enrollment")]
        public int? EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }

        public string URL { get; set; }
        public string Signature { get; set; }
        public DateTime AcceptedAt { get; set; }
    }
}
