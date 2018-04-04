using Onboarding.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModels
{
    public class GuarantorViewModel
    {
        public GuarantorViewModel()
        {
            Documents = new HashSet<DocumentViewModel>();
        }

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

        [Required]
        [Cpf]
        public string Cpf { get; set; }

        public string RelationshipId { get; set; }

        [Required]
        public IEnumerable<DocumentViewModel> Documents { get; set; }

    }
}
