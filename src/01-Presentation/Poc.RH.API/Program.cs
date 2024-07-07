using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using poc.core.api.net8;
using poc.core.api.net8.Extensions;
using Poc.Auth;
using Poc.Command;
using Poc.Contract.MappingProfile;
using Poc.DistributedCache;
using Poc.Oracle;
using Poc.Query;
using Poc.RabbiMQ;
using Poc.RH.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(Program).Assembly);

CommandInitializer.Initialize(builder.Services);
QueryInitializer.Initialize(builder.Services);
RabbiMQInitializer.Initialize(builder.Services);
CoreInitializer.Initialize(builder.Services);
IntegrationApisInitializer.Initialize(builder.Services);
DistributedCacheInitializer.Initialize(builder.Services, builder.Configuration);
OracleInitializer.Initialize(builder.Services, builder.Configuration);

builder.Services.AddHealthChecks(builder.Configuration);

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();
builder.Services.UseAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker") || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResponseWriter = (httpContext, healthReport) => httpContext.Response.WriteAsync(healthReport.ToJson())
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Logger.LogInformation("----- Iniciando a aplicação - RH...");
//await app.MigrateAsync(); // Aqui faz migrations

app.Run();

