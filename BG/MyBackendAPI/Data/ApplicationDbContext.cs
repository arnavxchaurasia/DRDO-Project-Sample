using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Models;

namespace MyBackendAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<PreProject> PreProjects { get; set; }
        public DbSet<ProjectSanction> ProjectSanctions { get; set; }
        public DbSet<ProjectClosure> ProjectClosures { get; set; }
        public DbSet<MonitoringReview> MonitoringReviews { get; set; }  // ✅ Added

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().ToTable("projects");
            modelBuilder.Entity<PreProject>().ToTable("pre_project");
            modelBuilder.Entity<ProjectSanction>().ToTable("project_sanction");
            modelBuilder.Entity<ProjectClosure>().ToTable("project_closure");
            modelBuilder.Entity<MonitoringReview>().ToTable("monitoringreview");  // ✅ Added
        }
    }
}
