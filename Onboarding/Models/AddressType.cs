using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class AddressType : BaseModel
    {
        public AddressType()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    } 
}