using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public abstract class Section : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Anchor { get; set; }
    }

    public class FinanceSection : Section
    {
    }

    public class AcademicSection : Section
    {
    }
}
