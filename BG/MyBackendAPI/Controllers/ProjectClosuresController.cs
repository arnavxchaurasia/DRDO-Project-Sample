using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.Models;
using MyBackendAPI.Dtos;



namespace MyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectClosuresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectClosuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectClosures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectClosure>>> GetProjectClosures()
        {
            return await _context.ProjectClosures
                                 .Include(p => p.Project)
                                 .ToListAsync();
        }

        // GET: api/ProjectClosures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectClosure>> GetProjectClosure(int id)
        {
            var closure = await _context.ProjectClosures
                                        .Include(p => p.Project)
                                        .FirstOrDefaultAsync(p => p.ProjectClosureId == id);

            if (closure == null)
                return NotFound();

            return closure;
        }

        // GET: api/ProjectClosures/project/3
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<ProjectClosure>> GetByProjectId(int projectId)
        {
            var closure = await _context.ProjectClosures
                                        .Include(p => p.Project)
                                        .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (closure == null)
                return NotFound();

            return closure;
        }

        // POST: api/ProjectClosures
       [HttpPost]
public async Task<ActionResult<ProjectClosure>> PostProjectClosure([FromForm] ProjectClosureUploadDto dto)
{
    var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == dto.ProjectId);
    if (!projectExists)
        return BadRequest("Invalid ProjectId.");

    var closure = new ProjectClosure
    {
        ProjectId = dto.ProjectId,
        IdcmApproval = dto.IdcmApproval,
        IdcmDate = dto.IdcmDate,
        IdcmMomFile = await ConvertToBytes(dto.IdcmMomFile),
        IdcmFile = await ConvertToBytes(dto.IdcmFile),
        IdcmRecommendation = dto.IdcmRecommendation,
        TcrApproval = dto.TcrApproval,
        TcrReport = dto.TcrReport,
        TcrFile = await ConvertToBytes(dto.TcrFile),
        AcFile = await ConvertToBytes(dto.AcFile),
        ClFile = await ConvertToBytes(dto.ClFile),
        ClDate = dto.ClDate,
        SubmittedAt = DateTime.Now
    };

    _context.ProjectClosures.Add(closure);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetProjectClosure), new { id = closure.ProjectClosureId }, closure);
}

private async Task<byte[]?> ConvertToBytes(IFormFile? file)
{
    if (file == null)
        return null;

    using var ms = new MemoryStream();
    await file.CopyToAsync(ms);
    return ms.ToArray();
}


        // PUT: api/ProjectClosures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectClosure(int id, ProjectClosure closure)
        {
            if (id != closure.ProjectClosureId)
                return BadRequest();

            _context.Entry(closure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectClosureExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/ProjectClosures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectClosure(int id)
        {
            var closure = await _context.ProjectClosures.FindAsync(id);
            if (closure == null)
                return NotFound();

            _context.ProjectClosures.Remove(closure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectClosureExists(int id)
        {
            return _context.ProjectClosures.Any(e => e.ProjectClosureId == id);
        }
    }
}
