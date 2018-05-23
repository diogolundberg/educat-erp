using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSO.Models
{
    public class Feature : BaseModel
    {
        public Feature()
        {
            GroupFeatures = new HashSet<GroupFeature>();
        }

        [Required]
        public string Grant { get; set; }

        public virtual ICollection<GroupFeature> GroupFeatures { get; set; }
    }
}
