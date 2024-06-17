using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Persistence.Context
{
    internal class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Domain.Entities.Persistence.Task>()
                   .Property(p => p.Title).IsRequired()
                   .HasMaxLength(50);
            builder.Entity<Domain.Entities.Persistence.Task>()
                   .Property(p => p.Description)
                   .HasMaxLength(100);
            builder.Entity<Domain.Entities.Persistence.Task>()
                   .Property(p => p.IsChecked)
                   .IsRequired();
            builder.Entity<Domain.Entities.Persistence.Task>()
                   .Property(p => p.CreatedOn)
                   .HasDefaultValueSql("getdate()");
            builder.Entity<Domain.Entities.Persistence.Task>()
                   .Property(p => p.CreatedBy)
                   .HasMaxLength(450);
        }

        public DbSet<Domain.Entities.Persistence.Task> Tasks { get; set; }
    }
}
