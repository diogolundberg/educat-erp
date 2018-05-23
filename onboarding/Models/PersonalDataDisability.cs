using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class PersonalDataDisability
    {
        [ForeignKey("Disability")]
        public int DisabilityId { get; set; }

        [JsonIgnore]
        public virtual Disability Disability { get; set; }

        [ForeignKey("PersonalData")]
        public int PersonalDataId { get; set; }

        [JsonIgnore]
        public virtual PersonalData PersonalData { get; set; }
    }
}
