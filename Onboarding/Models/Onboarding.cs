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

        public string StartAt { get; set; }

        public string EndAt { get; set; }

        public string Semester { get; set; }

        public string Year { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
                                                                        