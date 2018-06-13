
using Newtonsoft.Json;
using onboarding.ViewModels.Enrollments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace onboarding.Models
{
    public class Enrollment : BaseModel
    {
        public Enrollment()
        {
            EnrollmentSteps = new HashSet<EnrollmentStep>();
        }

        public virtual Payment Payment { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual PersonalData PersonalData { get; set; }
        public virtual FinanceData FinanceData { get; set; }
        public DateTime? SentAt { get; set; }
        public string Photo { get; set; }

        [ForeignKey("Onboarding")]
        public int? OnboardingId { get; set; }
        public virtual Onboarding Onboarding { get; set; }

        public override string CreateExternalId()
        {
            return Onboarding.Year + Onboarding.Semester + Regex.Replace(PersonalData.CPF, @"\D", string.Empty);
        }
        public bool IsDeadlineValid()
        {
            DateTime.TryParse(Onboarding.EndAt, out DateTime endAt);
            return DateTime.Now <= endAt;
        }

        public IEnumerable<EnrollmentStep> EnrollmentSteps { get; set; }
    }
}

