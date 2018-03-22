using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class Disability : BaseModel
    {
        public Disability ()
        {
            SpecialNeeds = new HashSet<SpecialNeed>();
        }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<SpecialNeed> SpecialNeeds { get; set; }
    }
}
