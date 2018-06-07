using System.Collections.Generic;

namespace onboarding.ViewModels.Enrollments
{
    public class Step
    {
        public Step()
        {
            Pendencies = new HashSet<Pendency>();
        }

        public string Name { get; set; }
        public string Resource { get; set; }
        public string Status { get; set; }
        public IEnumerable<Pendency> Pendencies { get; set; }
    }
}
