using System;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class BaseModel : ICloneable
    {
        public int ExternalId{ get; set; }

        [Key]
        public Guid Id { get; set; }

        public DateTime CommittedAt { get; set; }

        public string CommitedBy { get; set; }

        public string State { get; set; }

        public bool Active { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}