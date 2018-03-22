using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Gender : BaseModel
    {
        public Gender ()
        {
        }

        [Required]
        public string Name { get; set; }
    }
}
