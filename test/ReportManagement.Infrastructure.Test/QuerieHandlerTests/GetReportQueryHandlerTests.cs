using MassTransit;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.Dtos;
using ReportManagement.Application.QuerieHandler;
using ReportManagement.Application.Queries;
using ReportManagement.Domain.Repositorys;
using System;
using System.Threading.Tasks;

namespace ReportManagement.Infrastructure.Test.QuerieHandlerTests
{
    internal class GetReportQueryHandlerTests
    {
        [Test]
        public async Task GetReportQuery_GetReport()
        {
            //Arange
            var _readReportRepositoryMock = new Mock<IReadReportRepository>();

            var _commandMock = new Mock<ConsumeContext<IGetReportQuery>>();
            _commandMock.Setup(x => x.Message.Id).Returns(Guid.NewGuid());
            var handler = new GetReportQueryHandler(_readReportRepositoryMock.Object);

            //Act
            await handler.Consume(_commandMock.Object);

            //Asert
            _commandMock.Verify(x => x.RespondAsync<ReportDto>(It.IsAny<object>()), Times.Once);
        }
    }
}
