using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.Models;
using MyBackendAPI.DTOs;
using System.IO;

namespace MyBackendAPI.Controllers
{
 

    [Route("api/[controller]")]
    [ApiController]
    public class PreProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PreProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PreProjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreProject>>> GetPreProjects()
        {
            return await _context.PreProjects
                                 .Include(p => p.Project)
                                 .ToListAsync();
        }

        // GET: api/PreProjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreProject>> GetPreProject(int id)
        {
            var preProject = await _context.PreProjects
                                           .Include(p => p.Project)
                                           .FirstOrDefaultAsync(p => p.PreId == id);

            if (preProject == null)
                return NotFound();

            return preProject;
        }

        // GET: api/PreProjects/project/3
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<PreProject>> GetPreProjectByProjectId(int projectId)
        {
            var preProject = await _context.PreProjects
                                           .Include(p => p.Project)
                                           .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (preProject == null)
                return NotFound();

            return preProject;
        }

        // POST: api/PreProjects
       

[HttpPost("upload")]
[Consumes("multipart/form-data")]
public async Task<ActionResult<PreProject>> UploadPreProject([FromForm] PreProjectUploadDto dto)
{
    if (!await _context.Projects.AnyAsync(p => p.ProjectId == dto.ProjectId))
        return BadRequest("Invalid ProjectId.");

    var preProject = new PreProject
    {
        ProjectId = dto.ProjectId,
        Title = dto.Title,
        Description = dto.Description,
        DraftBrief = dto.DraftBrief,
        Date = dto.Date ?? DateTime.Now,

        MinutesFile = await ConvertToBytes(dto.MinutesFile),

        ExecutiveSummary = dto.ExecutiveSummary,
        ExecutiveSumFile = await ConvertToBytes(dto.ExecutiveSumFile),

        ObjectiveBrief = dto.ObjectiveBrief,
        ObjectiveFile = await ConvertToBytes(dto.ObjectiveFile),

        ScopeBrief = dto.ScopeBrief,
        ScopeFile = await ConvertToBytes(dto.ScopeFile),

        ParticipatingLabs = dto.ParticipatingLabs,
        User = dto.User,
        UserFile = await ConvertToBytes(dto.UserFile),

        CostBrief = dto.CostBrief,
        CostFile = await ConvertToBytes(dto.CostFile),
        Revenue = dto.Revenue,
        Capital = dto.Capital,
        FeDetails = dto.FeDetails,
        IcDetails = dto.IcDetails,
        ReDetails = dto.ReDetails,

        DurationBrief = dto.DurationBrief,
        DurationEstFile = await ConvertToBytes(dto.DurationEstFile),

        ManagementCounBrief = dto.ManagementCounBrief,
        ManagementApproval = dto.ManagementApproval,
        ManagementMinutesFile = await ConvertToBytes(dto.ManagementMinutesFile),
        CouncilFile = await ConvertToBytes(dto.CouncilFile),

        CcmApproval = dto.CcmApproval,
        CcmMinutesFile = await ConvertToBytes(dto.CcmMinutesFile),
        CcmDate = dto.CcmDate,

        PrcApproval = dto.PrcApproval,
        PrcMinutesFile = await ConvertToBytes(dto.PrcMinutesFile),
        PrcDate = dto.PrcDate,

        PdrBrief = dto.PdrBrief,
        PdrMinutesFile = await ConvertToBytes(dto.PdrMinutesFile),
        PdrFile = await ConvertToBytes(dto.PdrFile),
        PdrDate = dto.PdrDate,

        TiecBrief = dto.TiecBrief,
        TiecDate = dto.TiecDate,
        TiecMinutesFile = await ConvertToBytes(dto.TiecMinutesFile),

        CecBrief = dto.CecBrief,
        CecDate = dto.CecDate,
        CecMinutesFile = await ConvertToBytes(dto.CecMinutesFile),

        DmcBrief = dto.DmcBrief,
        DmcDate = dto.DmcDate,
        DmcMinutesFile = await ConvertToBytes(dto.DmcMinutesFile),

        SosBrief = dto.SosBrief,
        SosDate = dto.SosDate,
        SosMinutesFile = await ConvertToBytes(dto.SosMinutesFile),

        SubmittedAt = DateTime.Now
    };


            _context.PreProjects.Add(preProject);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetPreProject), new { id = preProject.PreId }, preProject);
}


        // PUT: api/PreProjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreProject(int id, PreProject preProject)
        {
            if (id != preProject.PreId)
                return BadRequest();

            _context.Entry(preProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreProjectExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/PreProjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreProject(int id)
        {
            var preProject = await _context.PreProjects.FindAsync(id);
            if (preProject == null)
                return NotFound();

            _context.PreProjects.Remove(preProject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private async Task<byte[]> ConvertToBytes(IFormFile file)
{
    if (file == null)
        return null;

    using (var memoryStream = new MemoryStream())
    {
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}


        private bool PreProjectExists(int id)
        {
            return _context.PreProjects.Any(e => e.PreId == id);
        }
    }
}
