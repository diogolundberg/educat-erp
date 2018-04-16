namespace Onboarding.ViewModels.Enrollments
{
    public class Record
    {
        public string OnboardingYear { get; set; }
        public string StartedAt { get; set; }
        public bool FirstTime { get; set; }
        public string Deadline { get; set; }
        public string DaysRemaining { get; set; }
        public string Photo { get; set; }
        public PersonalDataViewModel PersonalData { get; set; }
        public FinanceDataViewModel FinanceData { get; set; }
        public dynamic AcademicApproval { get; set; }
        public dynamic FinanceApproval { get; set; }
    }
}
