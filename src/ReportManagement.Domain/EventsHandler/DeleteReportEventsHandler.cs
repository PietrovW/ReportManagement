using MassTransit;
using MongoDB.Driver;
using ReportManagement.Application.Events;
using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Models;

namespace ReportManagement.Application.EventsHandler
{
    public class DeleteReportEventsHandler : IConsumer<IDeleteReportEvents>
    {
        private readonly IApplicationMongoDbContext _applicationMongoDbContext;
        public DeleteReportEventsHandler(IApplicationMongoDbContext applicationMongoDbContext)
        {
            _applicationMongoDbContext = applicationMongoDbContext;
        }

        public async Task Consume(ConsumeContext<IDeleteReportEvents> context)
        {
           IMongoCollection<ReportModel> collation = _applicationMongoDbContext.GetCollection<ReportModel>();

            _applicationMongoDbContext.Add(() => collation.DeleteOneAsync(x=>x.Id== context.Message.Id));
            await _applicationMongoDbContext.SaveChanges();
        }
    }
}
