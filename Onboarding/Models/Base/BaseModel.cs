using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class BaseModel : ICloneable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ExternalId { get; set; }

        [JsonIgnore]
        public DateTime CommittedAt { get; set; }

        [JsonIgnore]
        public string CommitedBy { get; set; }

        [JsonIgnore]
        public string DbState { get; set; }

        [JsonIgnore]
        public bool Active { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual string CreateExternalId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
