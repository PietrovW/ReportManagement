using AutoMapper;
using MassTransit;
using ReportManagement.Application.Dtos;
using ReportManagement.Application.Queries;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;

namespace ReportManagement.Application.QuerieHandler
{
    public class GetReportQueryHandler : IConsumer<IGetReportQuery>
    {
        private IMapper _mapper;
        private IReadReportRepository _reportRepository;
        public GetReportQueryHandler(IMapper mapper, IReadReportRepository reportRepository)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
        }
        public async Task Consume(ConsumeContext<IGetReportQuery> context)
        {
            ReportModel? reportModels = await _reportRepository.GetByIdAsync(context.Message.Id);
            await context.RespondAsync<ReportDto>(reportModels);
            

        }
    }
}
