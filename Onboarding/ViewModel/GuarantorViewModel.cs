using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public class GuarantorViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        public string ComplementAddress { get; set; }

        [Required]
        public string Neighborhood { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Landline { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]

        public int? CityId { get; set; }

        [Required]

        public int? StateId { get; set; }
    }
}
