namespace Onboarding.ViewModel
{
    public abstract class RepresentativeViewModel
    {
        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string ComplementAddress { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneNumber { get; set; }

        public string Landline { get; set; }

        public string Email { get; set; }

        public int? CityId { get; set; }

        public int? StateId { get; set; }
    }
}
