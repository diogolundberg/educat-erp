namespace onboarding.ViewModels.Appointments
{
    public class Record
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public int SchedulingId { get; set; }
        public bool Scheduled { get; set; }
    }
}
