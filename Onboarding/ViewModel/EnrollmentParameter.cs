using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public class EnrollmentParameter
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<EnrollmentParameterObj> List { get; set; }
    }

    public class EnrollmentParameterObj
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Name { get; set; }
    }
}