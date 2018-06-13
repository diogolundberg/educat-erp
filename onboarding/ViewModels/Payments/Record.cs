using System.Collections.Generic;

namespace onboarding.ViewModels.Payments
{
    public class Record
    {
        public Record()
        {
            Pendencies = new HashSet<Pendency>();
        }

        public string BilletUrl { get; set; }
        public string Url { get; set; }
        public IEnumerable<Pendency> Pendencies { get; set; }
    }
}
