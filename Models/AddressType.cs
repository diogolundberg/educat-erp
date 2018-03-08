using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class AddressType : BaseModel
    {
        [Required]
        public string Name { get; set; }
    } 
}