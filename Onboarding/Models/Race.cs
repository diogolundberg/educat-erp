using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Race : BaseModel
    {
        public Race ()
        {
            Enrollments = new HashSet<Enrollment>();    
        }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }    
}