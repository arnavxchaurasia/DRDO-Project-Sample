using MyBackendAPI.Models;

namespace MyBackendAPI.Services
{
    public enum ProjectLifecycleStatus
    {
        Draft,
        PreProject,
        Sanctioned,
        InProgress,
        Closed,
    }

    /// <summary>
    /// Derives a project's lifecycle stage from which of its one-to-one child
    /// records exist, rather than storing a separate status column that could
    /// drift out of sync with the records that actually define it.
    /// </summary>
    public static class ProjectLifecycleService
    {
        public static ProjectLifecycleStatus GetStatus(Project project)
        {
            if (project.ProjectClosure?.ClDate != null)
            {
                return ProjectLifecycleStatus.Closed;
            }
            if (project.MonitoringReview != null)
            {
                return ProjectLifecycleStatus.InProgress;
            }
            if (project.ProjectSanction != null)
            {
                return ProjectLifecycleStatus.Sanctioned;
            }
            if (project.PreProject != null)
            {
                return ProjectLifecycleStatus.PreProject;
            }
            return ProjectLifecycleStatus.Draft;
        }
    }
}
