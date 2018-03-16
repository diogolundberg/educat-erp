using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class Disability : BaseModel
    {
        public Disability ()
        {
            EnrollmentDisabilities = new HashSet<EnrollmentDisability>();
        }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<EnrollmentDisability> EnrollmentDisabilities { get; set; }
    } 
}