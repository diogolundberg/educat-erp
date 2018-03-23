using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public abstract class RepresentativePatchViewModel
    {
        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string ComplementAddress { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneNumber { get; set; }

        public string Landline { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string CityId { get; set; }

        public string StateId { get; set; }
    }
}
