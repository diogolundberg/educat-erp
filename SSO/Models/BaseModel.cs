using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SSO.Models
{
    public class BaseModel : ICloneable
    {
        public int ExternalId{ get; set; }

        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime CommittedAt { get; set; }

        [JsonIgnore]
        public string CommitedBy { get; set; }

        [JsonIgnore]
        public string State { get; set; }

        [JsonIgnore]
        public bool Active { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}