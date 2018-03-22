using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class Country : BaseModel
    {
        public Country ()
        {
        }

        [Required]
        public string Name { get; set; }
    }
}
