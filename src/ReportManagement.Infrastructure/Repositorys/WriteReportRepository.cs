using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;
using ReportManagement.Infrastructure.Data;

namespace ReportManagement.Infrastructure.Repositorys
{
    public class WriteReportRepository: WriteBaseRepository<ReportModel>, IWriteReportRepository
    {
        public WriteReportRepository(ApplicationDbContext dbContext) :base(dbContext)
        {

        }
    }
}
