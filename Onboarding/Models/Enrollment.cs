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
            EnrollmentPendencies = new HashSet<EnrollmentPendency>();
        }

        public virtual PersonalData PersonalData { get; set; }

        public virtual FinanceData FinanceData { get; set; }

        public DateTime? SentAt { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public DateTime? AcademicApproval { get; set; }

        public DateTime? FinanceApproval { get; set; }

        public DateTime Deadline { get; set; }

        public override string CreateExternalId()
        {
            string semester = DateTime.Now.Month > 6 ? "2" : "1";
            return DateTime.Now.Year + semester + Regex.Replace(PersonalData.CPF, @"\D", string.Empty); ;
        }

        internal bool IsDeadlineValid()
        {
            return DateTime.Now <= Deadline;
        }

        public IEnumerable<EnrollmentPendency> EnrollmentPendencies { get; set; }
    }
}

