using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class EnrollmentPendency
    {
        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        [ForeignKey("Pendency")]
        public int PendencyId { get; set; }

        public virtual Pendency Pendency { get; set; }
    }
}
