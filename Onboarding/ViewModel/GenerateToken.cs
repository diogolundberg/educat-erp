using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Onboarding.Validations;

namespace Onboarding.ViewModel
{
    public class GenerateToken
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<GenerateTokenEnrollment> List { get; set; }
    }

    public class GenerateTokenEnrollment
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Cpf]
        [Required]
        public string CPF { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
