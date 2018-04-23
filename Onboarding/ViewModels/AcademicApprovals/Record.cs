using System.Collections.Generic;
using Onboarding.Models;

namespace Onboarding.ViewModels.AcademicApprovals
{
    public class Record
    {
        public string EnrollmentNumber { get; set; }
        public IEnumerable<AcademicPendency> Pendencies { get; set; }

        public string Name { get; set; }
        public string AssumedName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Landline { get; set; }
        public string Photo { get; set; }

        public string BirthDate { get; set; }
        public string HighSchoolGraduationYear { get; set; }
        public string Zipcode { get; set; }
        public string StreetAddress { get; set; }
        public string AddressNumber { get; set; }
        public string ComplementAddress { get; set; }
        public string Neighborhood { get; set; }
        public string MothersName { get; set; }
        public string Handicap { get; set; }

        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        public string BirthCountry { get; set; }
        public string HighSchoolGraduationCountry { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressKind { get; set; }
        public string Race { get; set; }
        public string HighSchollKind { get; set; }
        public string Nationality { get; set; }
        public ICollection<string> SpecialNeeds { get; set; }
        public ICollection<string> Disabilities { get; set; }

        public ICollection<dynamic> Documents { get; set; }

        public string Status { get; set; }
    }
}
