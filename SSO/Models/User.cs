using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSO.Models
{
    public class User : BaseModel
    {
        public User()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
