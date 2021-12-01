using MassTransit;
using MongoDB.Driver;
using ReportManagement.Application.Events;
using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Models;

namespace ReportManagement.Application.EventsHandler
{
    public class CreateReportEventsHandler : IConsumer<ICreateReportEvents>
    {
        private readonly IApplicationMongoDbContext _applicationMongoDbContext;
        public CreateReportEventsHandler(IApplicationMongoDbContext applicationMongoDbContext)
        {
            _applicationMongoDbContext = applicationMongoDbContext;
        }

        public async Task Consume(ConsumeContext<ICreateReportEvents> context)
        {
            IMongoCollection<ReportModel> collation = _applicationMongoDbContext.GetCollection<ReportModel>();

            _applicationMongoDbContext.Add(() => collation.InsertOneAsync(new ReportModel() { Id = context.Message.Id, Name = context.Message.Name }));
            await _applicationMongoDbContext.SaveChanges();
        }
    }
}
