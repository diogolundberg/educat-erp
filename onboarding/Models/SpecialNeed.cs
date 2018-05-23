using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public class SpecialNeed : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [ForeignKey("Disability")]
        public int DisabilityId { get; set; }

        [JsonIgnore]
        public virtual Disability Disability { get; set; }
    }
}