using System.Collections.Generic;

namespace onboarding.ViewModels.Approvals
{
    public class Record
    {
        public Record()
        {
            Pendencies = new HashSet<Pendency>();
        }

        public int Id { get; set; }
        public string SentAt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Pendency> Pendencies { get; set; }
    }
}
