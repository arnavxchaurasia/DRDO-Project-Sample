using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackendAPI.Models
{
    public class PreProject
    {
        [Key]
        public int PreId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        [MaxLength(255)]
        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? DraftBrief { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public byte[]? MinutesFile { get; set; }

        public string? ExecutiveSummary { get; set; }
        public byte[]? ExecutiveSumFile { get; set; }

        public string? ObjectiveBrief { get; set; }
        public byte[]? ObjectiveFile { get; set; }

        public string? ScopeBrief { get; set; }
        public byte[]? ScopeFile { get; set; }

        public string? ParticipatingLabs { get; set; }
        public string? User { get; set; }
        public byte[]? UserFile { get; set; }

        public string? CostBrief { get; set; }
        public byte[]? CostFile { get; set; }

        public string? Revenue { get; set; }
        public string? Capital { get; set; }

        public string? FeDetails { get; set; }
        public string? IcDetails { get; set; }
        public string? ReDetails { get; set; }

        public string? DurationBrief { get; set; }
        public byte[]? DurationEstFile { get; set; }

        public string? ManagementCounBrief { get; set; }
        public string? ManagementApproval { get; set; }
        public byte[]? ManagementMinutesFile { get; set; }
        public byte[]? CouncilFile { get; set; }

        public string? CcmApproval { get; set; }
        public byte[]? CcmMinutesFile { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CcmDate { get; set; }

        public string? PrcApproval { get; set; }
        public byte[]? PrcMinutesFile { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PrcDate { get; set; }

        public string? PdrBrief { get; set; }
        public byte[]? PdrMinutesFile { get; set; }
        public byte[]? PdrFile { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PdrDate { get; set; }

        public string? TiecBrief { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TiecDate { get; set; }
        public byte[]? TiecMinutesFile { get; set; }

        public string? CecBrief { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CecDate { get; set; }
        public byte[]? CecMinutesFile { get; set; }

        public string? DmcBrief { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DmcDate { get; set; }
        public byte[]? DmcMinutesFile { get; set; }

        public string? SosBrief { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SosDate { get; set; }
        public byte[]? SosMinutesFile { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? SubmittedAt { get; set; }
    }
}
