using Microsoft.EntityFrameworkCore;
using ReportManagement.Domain.Models;
using ReportManagement.Infrastructure.Data.Configurations;

namespace ReportManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected ApplicationDbContext()
        {
        }
        public DbSet<ReportModel> Reports { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ReportTypeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
