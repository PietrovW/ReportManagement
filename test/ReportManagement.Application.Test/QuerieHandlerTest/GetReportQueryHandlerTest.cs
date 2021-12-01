using AutoMapper;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.AutoMapper;
using ReportManagement.Application.Dtos;
using ReportManagement.Application.QuerieHandler;
using ReportManagement.Application.Queries;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;
using System;
using System.Threading.Tasks;
using FluentAssertions;

namespace ReportManagement.Application.Test.QuerieHandlerTest
{
    [TestFixture]
    internal class GetReportQueryHandlerTest
    {
        [Test]
        public async Task CreateReportCommand_CustomerDataGetReport()
        {
            //Arange
            var reportRepositoryMoq = new Mock<IReadReportRepository>();
            Guid testId = Guid.NewGuid();
            ReportModel reportModel = new ReportModel() { Id = testId, Name = "test" };
            reportRepositoryMoq.Setup(f => f.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(reportModel));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<Profiles>());
            IMapper mapper = config.CreateMapper();
            var query = new GetReportQuery() { Id = testId };
            var handler = new GetReportQueryHandler(mapper, reportRepositoryMoq.Object);
            
            //Act
            ReportDto act = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asert
            act.Name.Should().Be("test");
        }

    }
}
