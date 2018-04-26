using System.ComponentModel.DataAnnotations;

namespace onboarding.ViewModels
{
    public class Pendency
    {
        public int Id { get; set; }

        [Required]
        public int? SectionId { get; set; }

        public string Description { get; set; }

        public string Anchor { get; set; }
    }
}
