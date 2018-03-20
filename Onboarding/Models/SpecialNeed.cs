using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class SpecialNeed : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public Guid DisabilityId { get; set; }

        [JsonIgnore]
        [ForeignKey("DisabilityId")]
        public virtual Disability Disability { get; set; }
    }
}