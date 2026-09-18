using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBackendAPI.Models;

public class MonitoringReview
{
    [Key]
    public int MonitoringReviewId { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public string? KickoffBrief { get; set; }

    [Column(TypeName = "date")]
    public DateTime? KickoffDate { get; set; }

    [Column(TypeName = "LONGBLOB")]
    public byte[]? KickoffPptFileName { get; set; }

    [Column(TypeName = "LONGBLOB")]
    public byte[]? KickoffMomFile { get; set; }

    public string? PmrcBrief { get; set; }

    [Column(TypeName = "date")]
    public DateTime? PmrcDate { get; set; }

    [Column(TypeName = "LONGBLOB")]
    public byte[]? PmrcPptFileName { get; set; }

    [Column(TypeName = "LONGBLOB")]
    public byte[]? PmrcMomFile { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SubmittedAt { get; set; } = DateTime.UtcNow;
}
