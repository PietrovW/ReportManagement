using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using ReportManagement.Application.Common;
using ReportManagement.Application.Events;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;

namespace ReportManagement.Application.CommandHandler
{
    public class CreateReportCommandHandler : IConsumer<ICreateReportCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWriteReportRepository _reportRepository;
        private readonly IMediator _mediator;
        public CreateReportCommandHandler(IMapper mapper, 
            IWriteReportRepository reportRepository, 
            IMediator mediator)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ICreateReportCommand> context)
        {
            ReportModel reportModel = _mapper.Map<ReportModel>(context.Message);
            Guid reportId = _reportRepository.Insert(reportModel);
            await _mediator.Publish<ICreateReportEvents>(new { Id = reportId, Name = context.Message.Name , Description= typeof(ICreateReportCommand).Name, CreatedDate = DateTime.UtcNow });
            await context.RespondAsync<ICreateReportStatus>(new
            {
                ReportId = reportId,
            });
        }
    }
}
