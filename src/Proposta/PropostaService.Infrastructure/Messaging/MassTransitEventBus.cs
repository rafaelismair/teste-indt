using MassTransit;
using PropostaService.Application.Ports;

namespace PropostaService.Infrastructure.Messaging;

public class MassTransitEventBus : IEventBus
{
    private readonly IPublishEndpoint _publish;

    public MassTransitEventBus(IPublishEndpoint publish)
    {
        _publish = publish;
    }

    public Task PublishAsync<T>(T message, CancellationToken ct) where T : class
        => _publish.Publish(message, ct);
}
