using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContratacaoService.Infrastructure.Messaging.Consumers;

namespace ContratacaoService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PropostaStatusAlteradoConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var host = configuration["RabbitMQ:Host"] ?? "localhost";
                var user = configuration["RabbitMQ:User"] ?? "guest";
                var pass = configuration["RabbitMQ:Pass"] ?? "guest";

                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(pass);
                });

                cfg.ReceiveEndpoint("contratacao-service.proposta-status-alterado", e =>
                {
                    e.ConfigureConsumer<PropostaStatusAlteradoConsumer>(context);
                });

            });
        });

        return services;
    }
}
