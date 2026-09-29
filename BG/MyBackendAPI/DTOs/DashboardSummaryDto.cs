namespace MyBackendAPI.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalProjects { get; set; }
        public Dictionary<string, int> ByStatus { get; set; } = new();
        public List<UpcomingReviewDto> UpcomingMonitoringReviews { get; set; } = new();
        public List<OverdueProjectDto> OverdueSanctionedProjects { get; set; } = new();
    }

    public class UpcomingReviewDto
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime PmrcDate { get; set; }
    }

    public class OverdueProjectDto
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime SanctionedSince { get; set; }
        public int DaysSinceSanction { get; set; }
    }
}
