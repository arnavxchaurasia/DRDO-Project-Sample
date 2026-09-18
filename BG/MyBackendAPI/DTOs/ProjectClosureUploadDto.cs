using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBackendAPI.Dtos
{
    public class ProjectClosureUploadDto
    {
        public int ProjectId { get; set; }

        public string? IdcmApproval { get; set; }

        public DateTime? IdcmDate { get; set; }

        public IFormFile? IdcmMomFile { get; set; }     // LONGBLOB

        public IFormFile? IdcmFile { get; set; }        // LONGBLOB

        public string? IdcmRecommendation { get; set; }

        public string? TcrApproval { get; set; }
        public string? TcrReport { get; set; }

        public IFormFile? TcrFile { get; set; }         // LONGBLOB

        public IFormFile? AcFile { get; set; }          // LONGBLOB

        public IFormFile? ClFile { get; set; }          // LONGBLOB

        public DateTime? ClDate { get; set; }
    }
}
