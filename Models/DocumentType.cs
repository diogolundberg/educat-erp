using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class DocumentType : BaseModel
    {
        public DocumentType ()
        {
            ResponsibleEnrollments = new HashSet<Enrollment>();
            GuarantorEnrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> ResponsibleEnrollments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> GuarantorEnrollments { get; set; }
    } 
}