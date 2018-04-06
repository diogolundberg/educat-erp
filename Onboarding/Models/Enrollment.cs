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

        public DateTime? SentAt { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public DateTime? AcademicApproval { get; set; }

        public DateTime? FinanceApproval { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime? StartAt { get; set; }

        public string Photo { get; set; }

        public override string CreateExternalId()
        {
            return Onboarding.Year + Onboarding.Semester + Regex.Replace(PersonalData.CPF, @"\D", string.Empty); ;
        }

        internal bool IsDeadlineValid()
        {
            return DateTime.Now <= Deadline;
        }

        public IEnumerable<Pendency> Pendencies { get; set; }

        public int? OnboardingId { get; set; }

        public virtual Onboarding Onboarding { get; set; }
    }
}

