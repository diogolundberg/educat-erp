using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class PersonalDataSpecialNeed
    {
        [ForeignKey("SpecialNeed")]
        public int SpecialNeedId { get; set; }

        [JsonIgnore]
        public virtual SpecialNeed SpecialNeed { get; set; }

        [ForeignKey("PersonalData")]
        public int PersonalDataId { get; set; }

        [JsonIgnore]
        public virtual PersonalData PersonalData { get; set; }
    }
}
