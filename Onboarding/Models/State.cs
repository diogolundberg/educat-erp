using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class State : BaseModel
    {
        public State ()
        {
            Cities = new HashSet<City>();
        }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<City> Cities { get; set; }
    }   
}