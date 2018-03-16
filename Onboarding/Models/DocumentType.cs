using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class DocumentType : BaseModel
    {
        public DocumentType ()
        {
            EnrollmentPeople = new HashSet<EnrollmentPerson>();
        }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<EnrollmentPerson> EnrollmentPeople { get; set; }
    } 
}