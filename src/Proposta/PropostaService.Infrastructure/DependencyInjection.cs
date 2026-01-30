using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaService.Application.Ports;
using PropostaService.Infrastructure.Adapters;
using PropostaService.Infrastructure.Messaging;
using PropostaService.Infrastructure.Persistence;

namespace PropostaService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<PropostaDbContext>(opt =>
            opt.UseNpgsql(config.GetConnectionString("Postgres")));

        services.AddScoped<IPropostaRepository, PropostaRepository>();
        services.AddScoped<IEventBus, MassTransitEventBus>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config["RabbitMQ:Host"], "/", h =>
                {
                    h.Username(config["RabbitMQ:User"] ?? "guest");
                    h.Password(config["RabbitMQ:Pass"] ?? "guest");
                });
            });
        });

        return services;
    }
}
