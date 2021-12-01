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
        private readonly IMapper _mapper;
        private readonly IWriteReportRepository _reportRepository;
        private readonly IReadReportRepository _readReportRepository;
        private readonly IMediator _mediator;
        public DeleteReportCommandHandler(IMapper mapper, 
            IWriteReportRepository reportRepository,
            IReadReportRepository readReportRepository,
            IMediator mediator)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _readReportRepository = readReportRepository;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<IDeleteReportCommand> context)
        {
            // ReportModel reportModel = _mapper.Map<ReportModel>(request);
            ReportModel? reportModel = await _readReportRepository.GetByIdAsync(context.Message.Id);
           // ReportModel reportModel = _mapper.Map<ReportModel>(request);
            _reportRepository.Delete(reportModel);
         await  _mediator.Publish<IDeleteReportEvents>(new  { Id = reportModel.Id, Description = typeof(IDeleteReportCommand).Name });
        }
    }
}
