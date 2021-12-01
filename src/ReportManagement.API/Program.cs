using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MassTransit;
using MassTransit.Mediator;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReportManagement.API.Extensions;
using ReportManagement.API.Request;
using ReportManagement.Application.Common;
using ReportManagement.Application.Dtos;
using ReportManagement.Application.Queries;
using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Repositorys;
using ReportManagement.Infrastructure.Data;
using ReportManagement.Infrastructure.Data.Settings;
using ReportManagement.Infrastructure.Repositorys;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoOptions>(
    builder.Configuration.GetSection(MongoOptions.Position));
Assembly[] assemblie = new Assembly[] { typeof(ReportManagement.Application.AutoMapper.Profiles).GetTypeInfo().Assembly };
builder.Services.AddValidatorsFromAssemblyContaining<CreateReportRequest>(lifetime: ServiceLifetime.Scoped);
builder.Services.AddValidatorsFromAssemblyContaining<ICreateReportCommand>(lifetime: ServiceLifetime.Scoped);
builder.Services.AddTransient(typeof(IReadBaseRepository<>), typeof(ReadBaseRepository<>));
builder.Services.AddTransient(typeof(IWriteBaseRepository<>), typeof(WriteBaseRepository<>));
builder.Services.AddTransient<IReadReportRepository, ReadReportRepository>();
builder.Services.AddTransient<IWriteReportRepository, WriteReportRepository>();
builder.Services.AddAutoMapper(assemblie);
builder.Services.AddFluentValidation(fv =>
{
    fv.DisableDataAnnotationsValidation = true;
    fv.AutomaticValidationEnabled = false;
});
builder.Services.Configure<MongoOptions>(
    builder.Configuration.GetSection(MongoOptions.Position));

builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Filename=ReportDatabase.db", options =>
 {
     options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
 }));
builder.Services.AddScoped<IApplicationMongoDbContext, ApplicationMongoDbContext>();
builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(assemblie);
});
var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.MapGet("api/reports/{id}", async (IMediator mediator, Guid id) =>
{

    var client = mediator.CreateRequestClient<IGetReportQuery>();
    var response = await client.GetResponse<ReportDto>(new { Id = id });

    return Results.Ok(response);
});
app.MapPost("api/reports", async (IValidator<CreateReportRequest> validator, IMediator mediator, LinkGenerator links, [FromBody] CreateReportRequest request) =>
{
    ValidationResult validationResult = validator.Validate(request);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult);
    }

    var client = mediator.CreateRequestClient<ICreateReportCommand>();
    var response = await client.GetResponse<ICreateReportStatus>(new { Name = request.Name });
    return Results.Created($"/reports/{response.Message.ReportId}", response.Message);
});
app.MapDelete("api/reports", async (IMediator mediator, Guid id) =>
{
    await mediator.Send<IDeleteReportCommand>(new { Id = id });
});
app.MigrateDatabase();
app.Run();
