﻿using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModels
{
    public class Pendency
    {
        public int Id { get; set; }

        [Required]
        public int? SectionId { get; set; }

        public string Description { get; set; }
    }
}