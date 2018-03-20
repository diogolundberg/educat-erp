using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class PersonalDataDisability
    {
        [Required]
        public Guid DisabilityId { get; set; }

        [JsonIgnore]
        [ForeignKey("DisabilityId")]
        public virtual Disability Disability { get; set; }

        [Required]
        public Guid PersonalDataId { get; set; }

        [JsonIgnore]
        [ForeignKey("PersonalDataId")]
        public virtual PersonalData PersonalData { get; set; }
    }
}
