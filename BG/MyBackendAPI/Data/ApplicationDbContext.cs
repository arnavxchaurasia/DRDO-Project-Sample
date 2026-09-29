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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().ToTable("projects");
            modelBuilder.Entity<PreProject>().ToTable("pre_project");
            modelBuilder.Entity<ProjectSanction>().ToTable("project_sanction");
            modelBuilder.Entity<ProjectClosure>().ToTable("project_closure");
            modelBuilder.Entity<MonitoringReview>().ToTable("monitoringreview");  // ✅ Added

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }

        /// <summary>
        /// Auto-stamps Project.CreatedAt/UpdatedAt so every caller gets audit
        /// timestamps for free instead of having to set them manually (and
        /// inevitably forgetting to on some code path).
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<Project>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
