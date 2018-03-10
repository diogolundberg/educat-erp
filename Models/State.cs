using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class State : BaseModel
    {
        public State ()
        {
            CountryStateEnrollments = new HashSet<Enrollment>();
            BornStateEnrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Enrollment> CountryStateEnrollments { get; set; }

        public virtual ICollection<Enrollment> BornStateEnrollments { get; set; }
    }   
}