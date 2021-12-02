using MassTransit;
using ReportManagement.Application.Dtos;
using ReportManagement.Application.Queries;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;

namespace ReportManagement.Application.QuerieHandler
{
    public class GetReportQueryHandler : IConsumer<IGetReportQuery>
    {
        private IReadReportRepository _reportRepository;
        public GetReportQueryHandler(IReadReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task Consume(ConsumeContext<IGetReportQuery> context)
        {
            ReportModel? reportModels = await _reportRepository.GetByIdAsync(context.Message.Id);
            await context.RespondAsync<ReportDto>(reportModels);
        }
    }
}
