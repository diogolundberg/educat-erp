using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    } 
}