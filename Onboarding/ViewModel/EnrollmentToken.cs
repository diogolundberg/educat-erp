using System;

namespace Onboarding.ViewModel
{
    public class EnrollmentToken
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsValid()
        {
            return DateTime.Now <= End;
        }
    }
}