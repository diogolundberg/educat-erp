using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class EnrollmentStep : BaseModel
    {
        public EnrollmentStep()
        {
            Pendencies = new HashSet<Pendency>();
        }

        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }

        [ForeignKey("Step")]
        public int StepId { get; set; }
        public virtual Step Step { get; set; }

        public virtual ICollection<Pendency> Pendencies { get; set; }
    }
}
