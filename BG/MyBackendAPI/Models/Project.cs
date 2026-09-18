using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackendAPI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        // ✅ One-to-One navigation to other stages
        public PreProject? PreProject { get; set; }
        public ProjectSanction? ProjectSanction { get; set; }
        public MonitoringReview? MonitoringReview { get; set; } // ✅ New stage
        public ProjectClosure? ProjectClosure { get; set; }
    }
}
