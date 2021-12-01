using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;

namespace ReportManagement.Infrastructure.Repositorys
{
    public class ReadReportRepository : ReadBaseRepository<ReportModel>, IReadReportRepository
    {
        public ReadReportRepository(IApplicationMongoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
