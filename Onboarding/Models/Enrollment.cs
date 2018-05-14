
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;

namespace onboarding.Models
{
    public class Enrollment : BaseModel
    {
        public Enrollment()
        {
            Pendencies = new HashSet<Pendency>();
            Appointments = new HashSet<Appointment>();
        }

        public virtual PersonalData PersonalData { get; set; }

        public virtual FinanceData FinanceData { get; set; }

        public DateTime? SentAt { get; set; }

        public DateTime? AcademicApproval { get; set; }

        public DateTime? FinanceApproval { get; set; }

        public DateTime? StartedAt { get; set; }

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

        [NotMapped]
        public IEnumerable<FinancePendency> FinancePendencies
        {
            get
            {
                return Pendencies.Where(x => x is FinancePendency).Select(x => (FinancePendency)x);
            }
        }

        [NotMapped]
        public IEnumerable<AcademicPendency> AcademicPendencies
        {
            get
            {
                return Pendencies.Where(x => x is AcademicPendency).Select(x => (AcademicPendency)x);
            }
        }

        public DateTime? EnrollmentInfo { get; set; }

        public DateTime? FinishedAt { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public int? InvoiceId { get; set; }
    }
}

