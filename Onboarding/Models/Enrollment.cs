
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Onboarding.Models
{
    public class Enrollment : BaseModel
    {
        public Enrollment()
        {
            Pendencies = new HashSet<Pendency>();
        }

        public virtual PersonalData PersonalData { get; set; }

        public virtual FinanceData FinanceData { get; set; }

        public string SentAt { get; set; }

        public string ReviewedAt { get; set; }

        public string AcademicApproval { get; set; }

        public string FinanceApproval { get; set; }

        public string StartedAt { get; set; }

        public string Photo { get; set; }

        public override string CreateExternalId()
        {
            return Onboarding.Year + Onboarding.Semester + Regex.Replace(PersonalData.CPF, @"\D", string.Empty); 
        }

        public bool IsDeadlineValid()
        {
            DateTime.TryParse(Onboarding.EndAt, out DateTime endAt);
            return DateTime.Now <= endAt;
        }

        public IEnumerable<Pendency> Pendencies { get; set; }

        public int? OnboardingId { get; set; }

        public virtual Onboarding Onboarding { get; set; }
    }
}

