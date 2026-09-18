using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackendAPI.Models
{
    public class ProjectSanction
{
    [Key]
    public int ProjectSanctionId { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public string? Category { get; set; }
    public string? DeliverableDetails { get; set; }

    public byte[]? DeliverableFile { get; set; }   // Changed to byte[]

    public string? ParticipatingLabName { get; set; }
    public string? SanctionDescription { get; set; }

    [Column(TypeName = "date")]
    public DateTime? SanctionDate { get; set; }

    public string? PdcDetails { get; set; }

    [Column(TypeName = "date")]
    public DateTime? PdcDate { get; set; }

    public string? SanctionApproval { get; set; }

    public byte[]? SanctionLetterFile { get; set; }  // Changed to byte[]

    public string? CorrigendumDescription { get; set; }

    public byte[]? CorrigendumFile { get; set; }     // Changed to byte[]

    public string? EbmBrief { get; set; }

    [Column(TypeName = "date")]
    public DateTime? EbmDate { get; set; }

    public byte[]? EbmPptFile { get; set; }         // Changed to byte[]
    public byte[]? EbmMomFile { get; set; }         // Changed to byte[]

    [Column(TypeName = "datetime")]
    public DateTime? SubmittedAt { get; set; }
}

}
