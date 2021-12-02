using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using ReportManagement.Application.Common;
using ReportManagement.Application.Events;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;

namespace ReportManagement.Application.CommandHandler
{
    public class DeleteReportCommandHandler : IConsumer<IDeleteReportCommand>
    {
        private readonly IWriteReportRepository _reportRepository;
        private readonly IReadReportRepository _readReportRepository;
        private readonly IMediator _mediator;
        public DeleteReportCommandHandler(
            IWriteReportRepository reportRepository,
            IReadReportRepository readReportRepository,
            IMediator mediator)
        {
            _reportRepository = reportRepository;
            _readReportRepository = readReportRepository;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<IDeleteReportCommand> context)
        {
            ReportModel? reportModel = await _readReportRepository.GetByIdAsync(context.Message.Id);
            _reportRepository.Delete(reportModel);
            await _mediator.Publish<IDeleteReportEvents>(new { Id = reportModel.Id, Description = typeof(IDeleteReportCommand).Name });
        }
    }
}
