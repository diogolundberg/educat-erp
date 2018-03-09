using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class State : BaseModel
    {
        public State ()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }   
}