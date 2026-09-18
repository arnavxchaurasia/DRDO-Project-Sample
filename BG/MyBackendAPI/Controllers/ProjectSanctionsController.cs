using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.Models;
using MyBackendAPI.DTOs;


namespace MyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSanctionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectSanctionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectSanctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectSanction>>> GetProjectSanctions()
        {
            return await _context.ProjectSanctions
                                 .Include(p => p.Project)
                                 .ToListAsync();
        }

        // GET: api/ProjectSanctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectSanction>> GetProjectSanction(int id)
        {
            var sanction = await _context.ProjectSanctions
                                         .Include(p => p.Project)
                                         .FirstOrDefaultAsync(p => p.ProjectSanctionId == id);

            if (sanction == null)
                return NotFound();

            return sanction;
        }

        // GET: api/ProjectSanctions/project/3
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<ProjectSanction>> GetByProjectId(int projectId)
        {
            var sanction = await _context.ProjectSanctions
                                         .Include(p => p.Project)
                                         .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (sanction == null)
                return NotFound();

            return sanction;
        }

        // POST: api/ProjectSanctions
       [HttpPost]
public async Task<ActionResult<ProjectSanction>> PostProjectSanction([FromForm] ProjectSanctionUploadDto dto)
{
    var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == dto.ProjectId);
    if (!projectExists)
        return BadRequest("Invalid ProjectId.");

    var sanction = new ProjectSanction
    {
        ProjectId = dto.ProjectId,
        Category = dto.Category,
        DeliverableDetails = dto.DeliverableDetails,
        DeliverableFile = await ConvertToByteArrayAsync(dto.DeliverableFile),

        ParticipatingLabName = dto.ParticipatingLabName,
        SanctionDescription = dto.SanctionDescription,
        SanctionDate = dto.SanctionDate,

        PdcDetails = dto.PdcDetails,
        PdcDate = dto.PdcDate,

        SanctionApproval = dto.SanctionApproval,
        SanctionLetterFile = await ConvertToByteArrayAsync(dto.SanctionLetterFile),

        CorrigendumDescription = dto.CorrigendumDescription,
        CorrigendumFile = await ConvertToByteArrayAsync(dto.CorrigendumFile),

        EbmBrief = dto.EbmBrief,
        EbmDate = dto.EbmDate,
        EbmPptFile = await ConvertToByteArrayAsync(dto.EbmPptFile),
        EbmMomFile = await ConvertToByteArrayAsync(dto.EbmMomFile),

        SubmittedAt = DateTime.Now
    };

    _context.ProjectSanctions.Add(sanction);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetProjectSanction), new { id = sanction.ProjectSanctionId }, sanction);
}

private async Task<byte[]?> ConvertToByteArrayAsync(IFormFile? file)
{
    if (file == null || file.Length == 0)
        return null;

    using var ms = new MemoryStream();
    await file.CopyToAsync(ms);
    return ms.ToArray();
}


        // PUT: api/ProjectSanctions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectSanction(int id, ProjectSanction sanction)
        {
            if (id != sanction.ProjectSanctionId)
                return BadRequest();

            _context.Entry(sanction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectSanctionExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/ProjectSanctions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectSanction(int id)
        {
            var sanction = await _context.ProjectSanctions.FindAsync(id);
            if (sanction == null)
                return NotFound();

            _context.ProjectSanctions.Remove(sanction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectSanctionExists(int id)
        {
            return _context.ProjectSanctions.Any(e => e.ProjectSanctionId == id);
        }
    }
}
