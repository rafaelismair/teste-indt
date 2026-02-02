using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContratacaoService.Infrastructure.Messaging.Consumers;
using ContratacaoService.Application.Ports;
using ContratacaoService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ContratacaoService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("Postgres")!;

        services.AddDbContext<ContratacaoDbContext>(opt =>
            opt.UseNpgsql(cs));

        services.AddScoped<IContratacaoRepository, ContratacaoRepository>();


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
