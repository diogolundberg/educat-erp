using System;
using System.Collections.Generic;

namespace Onboarding.Models
{
    public class Onboarding : BaseModel
    {
        public Onboarding()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        public string Semester { get; set; }

        public string Year { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
