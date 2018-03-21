using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class PersonalDataDocument
    {
        [Required]
        public Guid DocumentId { get; set; }

        [JsonIgnore]
        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }

        [Required]
        public Guid PersonalDataId { get; set; }

        [JsonIgnore]
        [ForeignKey("PersonalDataId")]
        public virtual PersonalData PersonalData { get; set; }
    }
}
