using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class PersonalDataSpecialNeed
    {
        [Required]
        public Guid SpecialNeedId { get; set; }

        [JsonIgnore]
        [ForeignKey("SpecialNeedId")]
        public virtual SpecialNeed SpecialNeed { get; set; }

        [Required]
        public Guid PersonalDataId { get; set; }

        [JsonIgnore]
        [ForeignKey("PersonalDataId")]
        public virtual PersonalData PersonalData { get; set; }
    }
}
