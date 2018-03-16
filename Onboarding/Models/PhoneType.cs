using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class PhoneType : BaseModel
    {
        public PhoneType () 
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }    
}