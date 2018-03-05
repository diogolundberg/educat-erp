using System;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CommittedAt { get; set; }

        public string CommitedBy { get; set; }

        public string Action { get; set; }
    }
}