using System.ComponentModel.DataAnnotations;

namespace onboarding.Models
{
    public class Gender : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public bool CheckMilitaryDraft { get; set; }
    }
}
