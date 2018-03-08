using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class CivilStatus : BaseModel
    {
        [Required]
        public string Name { get; set; }
    } 
}