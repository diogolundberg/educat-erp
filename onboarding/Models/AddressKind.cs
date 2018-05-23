using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace onboarding.Models
{
    public class AddressKind : BaseModel
    {
        public AddressKind()
        {
        }

        [Required]
        public string Name { get; set; }

    }
}
