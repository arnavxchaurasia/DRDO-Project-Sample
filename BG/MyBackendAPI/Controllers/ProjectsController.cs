using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.DTOs;
using MyBackendAPI.Models;
using MyBackendAPI.Services;

namespace MyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private const int MaxPageSize = 100;

        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects?page=1&pageSize=20&search=radar&status=InProgress
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProjectSummaryDto>>> GetProjects(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null,
            [FromQuery] ProjectLifecycleStatus? status = null)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

            var query = _context.Projects
                .Include(p => p.PreProject)
                .Include(p => p.ProjectSanction)
                .Include(p => p.MonitoringReview)
                .Include(p => p.ProjectClosure)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(p =>
                    (p.Name != null && EF.Functions.Like(p.Name, $"%{term}%")) ||
                    (p.Category != null && EF.Functions.Like(p.Category, $"%{term}%")));
            }

            // Status is a computed value (see ProjectLifecycleService), so it
            // can't be filtered in SQL — pull the (already narrowed) rows
            // client-side first, then filter/paginate in memory.
            var candidates = await query.OrderByDescending(p => p.StartDate).ToListAsync();

            var projected = candidates
                .Select(p => new ProjectSummaryDto
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Category = p.Category,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    Status = ProjectLifecycleService.GetStatus(p),
                    UpdatedAt = p.UpdatedAt,
                })
                .Where(p => status == null || p.Status == status)
                .ToList();

            var totalCount = projected.Count;
            var pageItems = projected
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new PagedResult<ProjectSummaryDto>
            {
                Items = pageItems,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
            });
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.ProjectId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
