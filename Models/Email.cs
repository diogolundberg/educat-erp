using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Email : BaseModel
    {
        public Email () 
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Value { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    } 
}