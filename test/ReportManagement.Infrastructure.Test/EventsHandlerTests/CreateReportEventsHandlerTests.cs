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
    internal class CreateReportEventsHandlerTests
    {
        [Test]
        public async Task CreateReportEvents_CreateReportNewItemMongoDb()
        {
            //Arange
            var _applicationMongoDbContextMock = new Mock<IApplicationMongoDbContext>();

            var _createReportEventMock = new Mock<ConsumeContext<ICreateReportEvents>>();
            _createReportEventMock.Setup(x => x.Message.Id).Returns(Guid.NewGuid());
            _createReportEventMock.Setup(x => x.Message.Name).Returns("test Name");
            _createReportEventMock.Setup(x => x.Message.Description).Returns("test Description");
            _createReportEventMock.Setup(x => x.Message.CreatedDate).Returns(DateTime.UtcNow);

            var _mongoCollectionMock = new Mock<IMongoCollection<ReportModel>>();
            
            _applicationMongoDbContextMock.Setup(x => x.GetCollection<ReportModel>()).Returns(_mongoCollectionMock.Object);

            var handler = new CreateReportEventsHandler(_applicationMongoDbContextMock.Object);

            //Act
            await handler.Consume(_createReportEventMock.Object);

            //Asert
            _applicationMongoDbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
