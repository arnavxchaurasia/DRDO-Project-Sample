using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackendAPI.Models
{
    public class ProjectClosure
{
    [Key]
    public int ProjectClosureId { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public string? IdcmApproval { get; set; }

    [Column(TypeName = "date")]
    public DateTime? IdcmDate { get; set; }

    public byte[]? IdcmMomFile { get; set; }   // CHANGED

    public byte[]? IdcmFile { get; set; }       // CHANGED

    public string? IdcmRecommendation { get; set; }

    public string? TcrApproval { get; set; }
    public string? TcrReport { get; set; }

    public byte[]? TcrFile { get; set; }        // CHANGED

    public byte[]? AcFile { get; set; }         // CHANGED

    public byte[]? ClFile { get; set; }         // CHANGED

    [Column(TypeName = "date")]
    public DateTime? ClDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SubmittedAt { get; set; }
}

}
