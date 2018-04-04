using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Gender : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public bool CheckMilitaryDraft { get; set; }
    }
}
