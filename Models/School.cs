using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class School : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }   
}