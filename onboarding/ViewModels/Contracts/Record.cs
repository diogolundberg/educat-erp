using System.Collections.Generic;

namespace onboarding.ViewModels.Contracts
{
    public class Record
    {
        public Record()
        {
            Pendencies = new HashSet<Pendency>();
        }

        public int? EnrollmentId { get; set; }
        public string URL { get; set; }
        public string Content { get; set; }
        public string Signature { get; set; }
        public string AcceptedAt { get; set; }
        public IEnumerable<Pendency> Pendencies { get; set; }
    }
}
