using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBackendAPI.DTOs
{
    public class ProjectSanctionUploadDto
    {
        public int ProjectId { get; set; }

        public string? Category { get; set; }
        public string? DeliverableDetails { get; set; }
        public IFormFile? DeliverableFile { get; set; }

        public string? ParticipatingLabName { get; set; }
        public string? SanctionDescription { get; set; }
        public DateTime? SanctionDate { get; set; }

        public string? PdcDetails { get; set; }
        public DateTime? PdcDate { get; set; }

        public string? SanctionApproval { get; set; }
        public IFormFile? SanctionLetterFile { get; set; }

        public string? CorrigendumDescription { get; set; }
        public IFormFile? CorrigendumFile { get; set; }

        public string? EbmBrief { get; set; }
        public DateTime? EbmDate { get; set; }

        public IFormFile? EbmPptFile { get; set; }
        public IFormFile? EbmMomFile { get; set; }
    }
}
