using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class City : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        [JsonIgnore]
        public virtual State State { get; set; }
    }
}
