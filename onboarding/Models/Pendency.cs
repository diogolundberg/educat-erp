using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public abstract class Pendency : BaseModel
    {
        [Required]
        [ForeignKey("Section")]
        public int SectionId { get; set; }

        [JsonIgnore]
        public virtual Section Section { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }
    }
}
