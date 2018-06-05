using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public class Pendency : BaseModel
    {
        [Required]
        [ForeignKey("Section")]
        public int SectionId { get; set; }

        [JsonIgnore]
        public virtual Section Section { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey("EnrollmentStep")]
        public int EnrollmentStepId { get; set; }

        public virtual EnrollmentStep EnrollmentStep { get; set; }
    }
}
