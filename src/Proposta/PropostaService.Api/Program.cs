using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PropostaService.Application.UseCases.CriarProposta;
using PropostaService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CriarPropostaCommand));


builder.Services.AddInfrastructure(builder.Configuration);

var postgresConnection = builder.Configuration.GetConnectionString("Postgres")!;

var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
var rabbitUser = builder.Configuration["RabbitMQ:User"] ?? "guest";
var rabbitPass = builder.Configuration["RabbitMQ:Pass"] ?? "guest";
var rabbitAmqp = $"amqp://{rabbitUser}:{rabbitPass}@{rabbitHost}:5672";

builder.Services.AddHealthChecks()
    .AddNpgSql(postgresConnection, name: "postgres", tags: new[] { "ready" })
    .AddRabbitMQ(rabbitAmqp, name: "rabbitmq", tags: new[] { "ready" });

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
});

app.MapControllers();
app.Run();
