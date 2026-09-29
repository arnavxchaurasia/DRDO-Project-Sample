using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.DTOs;
using MyBackendAPI.Services;

namespace MyBackendAPI.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    [Authorize] // requires a valid JWT — demonstrates the auth wiring end to end
    public class DashboardController : ControllerBase
    {
        // A sanctioned project with no monitoring review yet after this many
        // days is flagged as overdue — arbitrary but reasonable for a demo;
        // a real deployment would make this configurable per project type.
        private const int SanctionOverdueDays = 30;
        private const int UpcomingReviewWindowDays = 30;

        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/dashboard/summary
        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
        {
            var projects = await _context.Projects
                .Include(p => p.PreProject)
                .Include(p => p.ProjectSanction)
                .Include(p => p.MonitoringReview)
                .Include(p => p.ProjectClosure)
                .ToListAsync();

            var byStatus = projects
                .GroupBy(ProjectLifecycleService.GetStatus)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            // Ensure every status shows up (as 0) even with no matching projects,
            // so the frontend doesn't have to special-case a missing key.
            foreach (var status in Enum.GetValues<ProjectLifecycleStatus>())
            {
                byStatus.TryAdd(status.ToString(), 0);
            }

            var now = DateTime.UtcNow.Date;

            var upcomingReviews = projects
                .Where(p => p.MonitoringReview?.PmrcDate != null
                    && p.MonitoringReview.PmrcDate.Value >= now
                    && p.MonitoringReview.PmrcDate.Value <= now.AddDays(UpcomingReviewWindowDays))
                .Select(p => new UpcomingReviewDto
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.Name,
                    PmrcDate = p.MonitoringReview!.PmrcDate!.Value,
                })
                .OrderBy(r => r.PmrcDate)
                .ToList();

            var overdueSanctioned = projects
                .Where(p => ProjectLifecycleService.GetStatus(p) == ProjectLifecycleStatus.Sanctioned
                    && p.ProjectSanction?.SanctionDate != null
                    && p.ProjectSanction.SanctionDate.Value <= now.AddDays(-SanctionOverdueDays))
                .Select(p => new OverdueProjectDto
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.Name,
                    SanctionedSince = p.ProjectSanction!.SanctionDate!.Value,
                    DaysSinceSanction = (now - p.ProjectSanction.SanctionDate.Value).Days,
                })
                .OrderByDescending(r => r.DaysSinceSanction)
                .ToList();

            return Ok(new DashboardSummaryDto
            {
                TotalProjects = projects.Count,
                ByStatus = byStatus,
                UpcomingMonitoringReviews = upcomingReviews,
                OverdueSanctionedProjects = overdueSanctioned,
            });
        }
    }
}
