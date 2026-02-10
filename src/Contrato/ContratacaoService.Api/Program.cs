using ContratacaoService.Application;
using ContratacaoService.Application.Ports;
using ContratacaoService.Application.UseCases.CriarContratacao;
using ContratacaoService.Infrastructure;
using ContratacaoService.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(AssemblyReference).Assembly);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IContratacaoReadRepository, ContratacaoReadRepository>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
