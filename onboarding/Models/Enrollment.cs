
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
            Pendencies = new HashSet<Pendency>();
            EnrollmentSteps = new HashSet<EnrollmentStep>();
        }
        public virtual Contract Contract { get; set; }
        public virtual PersonalData PersonalData { get; set; }
        public virtual FinanceData FinanceData { get; set; }
        public DateTime? SentAt { get; set; }
        public string Photo { get; set; }

        public IEnumerable<Pendency> Pendencies { get; set; }

        [ForeignKey("Onboarding")]
        public int? OnboardingId { get; set; }
        public virtual Onboarding Onboarding { get; set; }

        public int? InvoiceId { get; set; }
        public Invoice Invoice
        {
            get
            {
                if (InvoiceId == null)
                {
                    return null;
                }

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = client.GetAsync(Environment.GetEnvironmentVariable("FINANCE_HOST") + "/api/invoices/" + InvoiceId).Result;

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    return JsonConvert.DeserializeObject<Invoice>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public override string CreateExternalId()
        {
            return Onboarding.Year + Onboarding.Semester + Regex.Replace(PersonalData.CPF, @"\D", string.Empty);
        }
        public bool IsDeadlineValid()
        {
            DateTime.TryParse(Onboarding.EndAt, out DateTime endAt);
            return DateTime.Now <= endAt;
        }

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

        public IEnumerable<EnrollmentStep> EnrollmentSteps { get; set; }
    }
}

