using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class School : BaseModel
    {
        public School () 
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }   
}