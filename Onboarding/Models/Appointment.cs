namespace onboarding.Models
{
    public class Appointment : BaseModel
    {
        public int? EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public int SchedulingId { get; set; }
        public Scheduling Scheduling { get; set; }
    }
}
