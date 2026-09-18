using Microsoft.AspNetCore.Http;
using System;

namespace MyBackendAPI.Models
{
    public class MonitoringReviewUploadDto
    {
        public int ProjectId { get; set; }
        public string? KickoffBrief { get; set; }
        public DateTime? KickoffDate { get; set; }

        public IFormFile? KickoffPptFileName { get; set; }
        public IFormFile? KickoffMomFile { get; set; }

        public string? PmrcBrief { get; set; }
        public DateTime? PmrcDate { get; set; }

        public IFormFile? PmrcPptFileName { get; set; }
        public IFormFile? PmrcMomFile { get; set; }
    }
}
