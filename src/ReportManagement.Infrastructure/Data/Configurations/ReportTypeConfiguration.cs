using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportManagement.Domain.Models;

namespace ReportManagement.Infrastructure.Data.Configurations
{
    public class ReportTypeConfiguration : IEntityTypeConfiguration<ReportModel>
    {
        public void Configure(EntityTypeBuilder<ReportModel> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Id).HasColumnType<Guid>("Guid").ValueGeneratedOnAdd().IsRequired();
        }
    }
}
