namespace onboarding.ViewModels.Enrollments
{
    public class Record
    {
        public string OnboardingYear { get; set; }
        public string StartedAt { get; set; }
        public string SentAt { get; set; }
        public bool FirstTime { get; set; }
        public bool EnrollmentInfo { get; set; }
        public bool Finished { get; set; }
        public bool Scheduled { get; set; }
        public string Deadline { get; set; }
        public string DaysRemaining { get; set; }
        public string Photo { get; set; }
        public int? InvoiceId { get; set; }
        public PersonalDataViewModel PersonalData { get; set; }
        public FinanceDataViewModel FinanceData { get; set; }
        public dynamic AcademicApproval { get; set; }
        public dynamic FinanceApproval { get; set; }
        public dynamic PaymentApproval { get; set; }
    }
}
