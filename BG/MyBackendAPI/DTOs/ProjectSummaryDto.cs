using MyBackendAPI.Services;

namespace MyBackendAPI.DTOs
{
    public class ProjectSummaryDto
    {
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public ProjectLifecycleStatus Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
