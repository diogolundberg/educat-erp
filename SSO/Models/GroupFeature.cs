using System;
using SSO.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Models
{
    public class GroupFeature
    {
        [Required]
        public Guid FeatureId { get; set; }

        [ForeignKey("FeatureId")]
        public virtual Feature Feature { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
    }
}