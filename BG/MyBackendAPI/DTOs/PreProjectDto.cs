using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBackendAPI.DTOs
{
    public class PreProjectUploadDto
    {
        public int ProjectId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DraftBrief { get; set; }
        public DateTime? Date { get; set; } 

        public IFormFile? MinutesFile { get; set; }
        public string? ExecutiveSummary { get; set; }
        public IFormFile? ExecutiveSumFile { get; set; }
        public string? ObjectiveBrief { get; set; }
        public IFormFile? ObjectiveFile { get; set; }
        public string? ScopeBrief { get; set; }
        public IFormFile? ScopeFile { get; set; }
        public string? ParticipatingLabs { get; set; }
        public string? User { get; set; }
        public IFormFile? UserFile { get; set; }
        public string? CostBrief { get; set; }
        public IFormFile? CostFile { get; set; }
        public string? Revenue { get; set; }
        public string? Capital { get; set; }
        public string? FeDetails { get; set; }
        public string? IcDetails { get; set; }
        public string? ReDetails { get; set; }
        public string? DurationBrief { get; set; }
        public IFormFile? DurationEstFile { get; set; }
        public string? ManagementCounBrief { get; set; }
        public string? ManagementApproval { get; set; }
        public IFormFile? ManagementMinutesFile { get; set; }
        public IFormFile? CouncilFile { get; set; }
        public string? CcmApproval { get; set; }
        public IFormFile? CcmMinutesFile { get; set; }
        public DateTime? CcmDate { get; set; }
        public string? PrcApproval { get; set; }
        public IFormFile? PrcMinutesFile { get; set; }
        public DateTime? PrcDate { get; set; }
        public string? PdrBrief { get; set; }
        public IFormFile? PdrMinutesFile { get; set; }
        public IFormFile? PdrFile { get; set; }
        public DateTime? PdrDate { get; set; }
        public string? TiecBrief { get; set; }
        public DateTime? TiecDate { get; set; }
        public IFormFile? TiecMinutesFile { get; set; }
        public string? CecBrief { get; set; }
        public DateTime? CecDate { get; set; }
        public IFormFile? CecMinutesFile { get; set; }
        public string? DmcBrief { get; set; }
        public DateTime? DmcDate { get; set; }
        public IFormFile? DmcMinutesFile { get; set; }
        public string? SosBrief { get; set; }
        public DateTime? SosDate { get; set; }
        public IFormFile? SosMinutesFile { get; set; }
    }
}
