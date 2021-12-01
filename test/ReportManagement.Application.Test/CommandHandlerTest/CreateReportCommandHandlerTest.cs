using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.AutoMapper;
using ReportManagement.Application.CommandHandler;
using ReportManagement.Application.Common;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;
using System;
using System.Threading.Tasks;

namespace ReportManagement.Application.Test.CommandHandlerTest
{
    [TestFixture]
    internal class CreateReportCommandHandlerTest
    {
        [Test]
        public async Task CreateReportCommand_CustomerDataCreateOnDatabase()
        {
            //Arange
            var reportRepositoryMoq = new Mock<IWriteReportRepository>();
            var mediatorMoq = new Mock<IMediator>();
            Guid testId= Guid.NewGuid();
            reportRepositoryMoq.Setup(f => f.Insert(It.IsAny<ReportModel>()))
                .Returns(testId);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<Profiles>());
            IMapper mapper = config.CreateMapper();
            var command = new CreateReportCommand() { Name ="test"};
            var handler = new CreateReportCommandHandler(mapper, reportRepositoryMoq.Object, mediatorMoq.Object);

            //Act
            Guid id = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            reportRepositoryMoq.Verify(x => x.Insert(It.IsAny<ReportModel>()));
        }
    }
}
