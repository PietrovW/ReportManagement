using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.AutoMapper;
using ReportManagement.Application.CommandHandler;
using ReportManagement.Application.Common;
using ReportManagement.Domain.Repositorys;
using System.Threading.Tasks;

namespace ReportManagement.Infrastructure.Test.CommandHandlerTest
{
    internal class CreateReportCommandHandlerTests
    {
        [Test]
        public async Task CreateReportCommand_CustomerDataUpdatedOnDatabase()
        {
            //Arange
            var _mediatorMock = new Mock<IMediator>();
            var _writeReportRepositoryMock = new Mock<IWriteReportRepository>();
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(Profiles).Assembly);
            });

            var _commandMock = new Mock<ConsumeContext<ICreateReportCommand>>();
            _commandMock.Setup(x => x.Message.Name).Returns("test");
            var handler = new CreateReportCommandHandler(config.CreateMapper(), _writeReportRepositoryMock.Object, _mediatorMock.Object);

            //Act
            await handler.Consume(_commandMock.Object);

            //Asert
            _commandMock.Verify(x => x.RespondAsync<ICreateReportStatus>(It.IsAny<object>()), Times.Once);
        }
    }
}
