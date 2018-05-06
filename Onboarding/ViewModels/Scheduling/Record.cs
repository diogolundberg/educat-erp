using System.Collections.Generic;

namespace onboarding.ViewModels.Scheduling
{
    public class Record
    {
        public int Id { get; set; }
        public string Onboarding { get; set; }
        public string OnboardingId { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public string ScheduleStartTime { get; set; }
        public string ScheduleEndTime { get; set; }
        public string Intervals { get; set; }
        public List<FormBreak> Breaks { get; set; }
    }
}