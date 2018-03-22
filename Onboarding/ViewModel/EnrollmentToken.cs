using Onboarding.Models;
using System;

namespace Onboarding.ViewModel
{
    public class EnrollmentToken
    {
        public string ExternalId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool IsValid(PersonalData personalData)
        {
            return DateTime.Now <= End;
        }
    }
}
