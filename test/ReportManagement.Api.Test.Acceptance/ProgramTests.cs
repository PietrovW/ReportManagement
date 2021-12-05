using AutoMapper;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using ReportManagement.Application.AutoMapper;
using ReportManagement.Application.CommandHandler;
using ReportManagement.Application.Common;
using ReportManagement.Application.Dtos;
using ReportManagement.Application.Events;
using ReportManagement.Application.QuerieHandler;
using ReportManagement.Application.Queries;
using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportManagement.Api.Test.Acceptance
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task Should_publish_the_GetReportQuery_Resturn_ReportDto()
        {
            // Arrange
            Guid idtest = Guid.NewGuid();
            var readReportRepositoryMoq = new Mock<IReadReportRepository>();
            readReportRepositoryMoq.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Models.ReportModel() { Id = idtest, Name = "test" });
            var provider = new ServiceCollection()
                .AddTransient(provider =>
                {
                    return (IReadReportRepository)readReportRepositoryMoq.Object;
                })
                .AddMassTransitInMemoryTestHarness(cfg =>
                {
                    cfg.AddConsumer<GetReportQueryHandler>();
                })
                .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<InMemoryTestHarness>();

            await harness.Start();
            try
            {
                var bus = provider.GetRequiredService<IBus>();

                var client = bus.CreateRequestClient<IGetReportQuery>();

                var response = await client.GetResponse<ReportDto>(new { Id = idtest });

                // Assert
                Assert.That(await harness.Consumed.Any<ReportDto>());
            }
            finally
            {
                await harness.Stop();

                await provider.DisposeAsync();
            }
        }

        [Test]
        public async Task Should_publish_the_CreateReportCommand_Resturn_CreateReportStatus()
        {
            // Arrange
            Guid idtest = Guid.NewGuid();
            var _applicationMongoDbContextMock = new Mock<IApplicationMongoDbContext>();

            var _writeReportRepositoryMock = new Mock<IWriteReportRepository>();
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(Profiles).Assembly);
            });

            var _mongoCollectionMock = new Mock<IMongoCollection<ReportModel>>();

            _applicationMongoDbContextMock.Setup(x => x.GetCollection<ReportModel>()).Returns(_mongoCollectionMock.Object);

            var provider = new ServiceCollection()
                .AddScoped(provider =>
                {
                    return config.CreateMapper();
                })
                .AddTransient(provider =>
                {
                    return _writeReportRepositoryMock.Object;
                })
                .AddMediator(cfg =>
                {
                    cfg.AddConsumer<CreateReportCommandHandler>();
                })
                .AddMassTransitInMemoryTestHarness(cfg =>
                {
                    cfg.AddConsumer<CreateReportCommandHandler>();
                    

                })
                .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<InMemoryTestHarness>();

            await harness.Start();
            try
            {
                var bus = provider.GetRequiredService<IBus>();

                var client = bus.CreateRequestClient<ICreateReportCommand>();
                var response = await client.GetResponse<ICreateReportStatus>(new { Name = "Test" });

                // Assert
                Assert.That(await harness.Consumed.Any<ICreateReportStatus>());

            }
            finally
            {
                await harness.Stop();

                await provider.DisposeAsync();
            }
        }
    }
}