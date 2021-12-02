using MassTransit;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.Events;
using ReportManagement.Application.EventsHandler;
using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ReportManagement.Infrastructure.Test.EventsHandlerTests
{
    internal class DeleteReportEventsHandlerTests
    {
        [Test]
        public async Task DeleteReportEvents_DeleteReportItemMongoDb()
        {
            //Arange
            var _applicationMongoDbContextMock = new Mock<IApplicationMongoDbContext>();

            var _deleteReportEventMock = new Mock<ConsumeContext<IDeleteReportEvents>>();
            _deleteReportEventMock.Setup(x => x.Message.Id).Returns(Guid.NewGuid());
            _deleteReportEventMock.Setup(x => x.Message.Description).Returns("test Description");
            _deleteReportEventMock.Setup(x => x.Message.CreatedDate).Returns(DateTime.UtcNow);

            var _mongoCollectionMock = new Mock<IMongoCollection<ReportModel>>();

            _applicationMongoDbContextMock.Setup(x => x.GetCollection<ReportModel>()).Returns(_mongoCollectionMock.Object);

            var handler = new DeleteReportEventsHandler(_applicationMongoDbContextMock.Object);

            //Act
            await handler.Consume(_deleteReportEventMock.Object);

            //Asert
            _applicationMongoDbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
