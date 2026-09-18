using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.Models;

namespace MyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MonitoringReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MonitoringReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonitoringReview>>> GetMonitoringReviews()
        {
            return await _context.MonitoringReviews
                                 .Include(m => m.Project)
                                 .ToListAsync();
        }

        // GET: api/MonitoringReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonitoringReview>> GetMonitoringReview(int id)
        {
            var review = await _context.MonitoringReviews
                                       .Include(m => m.Project)
                                       .FirstOrDefaultAsync(m => m.MonitoringReviewId == id);

            if (review == null)
                return NotFound();

            return review;
        }

        // GET: api/MonitoringReviews/project/3
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<MonitoringReview>> GetByProjectId(int projectId)
        {
            var review = await _context.MonitoringReviews
                                       .Include(m => m.Project)
                                       .FirstOrDefaultAsync(m => m.ProjectId == projectId);

            if (review == null)
                return NotFound();

            return review;
        }

        // GET: api/MonitoringReviews/DownloadFile/10002/kickoffppt
        [HttpGet("DownloadFile/{projectId}/{fileType}")]
        public async Task<IActionResult> DownloadFile(int projectId, string fileType)
        {
            var review = await _context.MonitoringReviews.FirstOrDefaultAsync(m => m.ProjectId == projectId);
            if (review == null) return NotFound("Monitoring Review not found");

            byte[] fileBytes = null;
            string fileName = "";
            string contentType = "application/octet-stream";

            switch (fileType.ToLower())
            {
                case "kickoffppt":
                    fileBytes = review.KickoffPptFileName;
                    fileName = "KickoffPresentation.pdf";
                    contentType = "application/pdf";
                    break;
                case "kickoffmom":
                    fileBytes = review.KickoffMomFile;
                    fileName = "KickoffMOM.docx";
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "pmrcppt":
                    fileBytes = review.PmrcPptFileName;
                    fileName = "PMRCPresentation.pdf";
                    contentType = "application/pdf";
                    break;
                case "pmrcmom":
                    fileBytes = review.PmrcMomFile;
                    fileName = "PMRCMOM.docx";
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                default:
                    return BadRequest("Invalid file type.");
            }

            if (fileBytes == null) return NotFound("File not found.");
            return File(fileBytes, contentType, fileName);
        }

        // GET: api/MonitoringReviews/FileBase64/10002/kickoffppt
        [HttpGet("FileBase64/{projectId}/{fileType}")]
        public async Task<IActionResult> GetFileBase64(int projectId, string fileType)
        {
            var review = await _context.MonitoringReviews.FirstOrDefaultAsync(p => p.ProjectId == projectId);
            if (review == null)
                return NotFound("MonitoringReview not found");

            byte[] fileBytes = null;
            string contentType = "application/octet-stream";

            switch (fileType.ToLower())
            {
                case "kickoffppt":
                    fileBytes = review.KickoffPptFileName;
                    contentType = "application/pdf";
                    break;
                case "kickoffmom":
                    fileBytes = review.KickoffMomFile;
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "pmrcppt":
                    fileBytes = review.PmrcPptFileName;
                    contentType = "application/pdf";
                    break;
                case "pmrcmom":
                    fileBytes = review.PmrcMomFile;
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                default:
                    return BadRequest("Invalid file type");
            }

            if (fileBytes == null || fileBytes.Length == 0)
                return NotFound("File is empty");

            var base64 = Convert.ToBase64String(fileBytes);
            return Ok(new { base64, contentType });
        }

        // POST: api/MonitoringReviews
        [HttpPost]
        public async Task<ActionResult<MonitoringReview>> PostMonitoringReview([FromForm] MonitoringReviewUploadDto dto)
        {
            var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == dto.ProjectId);
            if (!projectExists)
                return BadRequest("Invalid ProjectId.");

            var review = new MonitoringReview
            {
                ProjectId = dto.ProjectId,
                KickoffBrief = dto.KickoffBrief,
                KickoffDate = dto.KickoffDate,
                PmrcBrief = dto.PmrcBrief,
                PmrcDate = dto.PmrcDate,
                SubmittedAt = DateTime.UtcNow
            };

            if (dto.KickoffPptFileName != null)
                review.KickoffPptFileName = await ConvertToBytes(dto.KickoffPptFileName);

            if (dto.KickoffMomFile != null)
                review.KickoffMomFile = await ConvertToBytes(dto.KickoffMomFile);

            if (dto.PmrcPptFileName != null)
                review.PmrcPptFileName = await ConvertToBytes(dto.PmrcPptFileName);

            if (dto.PmrcMomFile != null)
                review.PmrcMomFile = await ConvertToBytes(dto.PmrcMomFile);

            _context.MonitoringReviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMonitoringReview), new { id = review.MonitoringReviewId }, review);
        }

        private async Task<byte[]> ConvertToBytes(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        // PUT: api/MonitoringReviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonitoringReview(int id, MonitoringReview review)
        {
            if (id != review.MonitoringReviewId)
                return BadRequest();

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonitoringReviewExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/MonitoringReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitoringReview(int id)
        {
            var review = await _context.MonitoringReviews.FindAsync(id);
            if (review == null)
                return NotFound();

            _context.MonitoringReviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonitoringReviewExists(int id)
        {
            return _context.MonitoringReviews.Any(e => e.MonitoringReviewId == id);
        }
    }
}
