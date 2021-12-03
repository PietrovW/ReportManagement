using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.CommandHandler;
using ReportManagement.Application.Common;
using ReportManagement.Application.Events;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;
using System;
using System.Collections.Generic;
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
            var listReportModel = new List<ReportModel?>();
            var idtest = Guid.NewGuid();
            _readReportRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync( new ReportModel() { Id = idtest, Name = "test" });
            var _writeReportRepositoryMock = new Mock<IWriteReportRepository>();

            var _commandMock = new Mock<ConsumeContext<IDeleteReportCommand>>();
            _commandMock.Setup(x => x.Message.Id).Returns(Guid.NewGuid());
            var handler = new DeleteReportCommandHandler(_writeReportRepositoryMock.Object, _readReportRepository.Object, _mediatorMock.Object);

            //Act
            await handler.Consume(_commandMock.Object);

            //Asert
            _mediatorMock.Verify(x => x.Publish<IDeleteReportEvents>(It.IsAny<object>(), default(CancellationToken)), Times.Once);
        }
    }
}
