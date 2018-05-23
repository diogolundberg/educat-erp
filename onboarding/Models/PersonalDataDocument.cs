using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class PersonalDataDocument
    {
        [ForeignKey("Document")]
        public int DocumentId { get; set; }

        [JsonIgnore]
        public virtual Document Document { get; set; }

        [ForeignKey("PersonalData")]
        public int PersonalDataId { get; set; }

        [JsonIgnore]
        public virtual PersonalData PersonalData { get; set; }
    }
}
