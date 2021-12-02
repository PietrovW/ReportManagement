using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.AutoMapper;
using ReportManagement.Application.CommandHandler;
using ReportManagement.Application.Common;
using ReportManagement.Application.Events;
using ReportManagement.Domain.Repositorys;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReportManagement.Infrastructure.Test.CommandHandlerTest
{
    internal class DeleteReportCommandHandlerTests
    {
        [Test]
        public async Task DeleteReportCommand_CustomerDataDeleteOnDatabase()
        {
            //Arange
            var _mediatorMock = new Mock<IMediator>();
            var _readReportRepository = new Mock<IReadReportRepository>();
            var _writeReportRepositoryMock = new Mock<IWriteReportRepository>();
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(Profiles).Assembly);
            });

            var _commandMock = new Mock<ConsumeContext<IDeleteReportCommand>>();
            _commandMock.Setup(x => x.Message.Id).Returns(Guid.NewGuid());
            var handler = new DeleteReportCommandHandler(_writeReportRepositoryMock.Object, _readReportRepository.Object, _mediatorMock.Object);

            //Act
            await handler.Consume(_commandMock.Object);

            //Asert
            _commandMock.Verify(x => x.Publish<IDeleteReportEvents>(It.IsAny<object>(), default(CancellationToken)), Times.Once);
        }
    }
}
