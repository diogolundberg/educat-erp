using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class EnrollmentStep : BaseModel
    {
        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        [ForeignKey("Step")]
        public int StepId { get; set; }

        public virtual Step Step { get; set; }
    }
}
