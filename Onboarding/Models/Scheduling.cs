using Newtonsoft.Json;
using onboarding.ViewModels.Scheduling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public class Scheduling : BaseModel
    {
        public Scheduling()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int OnboardingId { get; set; }
        public virtual Onboarding Onboarding { get; set; }
        public DateTime StartAt { get; set; }
        public string ScheduleStartTime { get; set; }
        public string ScheduleEndTime { get; set; }
        public DateTime EndAt { get; set; }
        public string Intervals { get; set; }
        public string Breaks { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        [NotMapped]
        public List<FormBreak> FormBreaks
        {
            get
            {
                return !string.IsNullOrEmpty(Breaks) ? JsonConvert.DeserializeObject<List<FormBreak>>(Breaks) : new List<FormBreak>();
            }
        }
    }
}