using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSO.Models
{
    public class Group : BaseModel
    {
        public Group()
        {
            Permissions = new HashSet<GroupFeature>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<GroupFeature> Permissions { get; set; }
    }    
}
