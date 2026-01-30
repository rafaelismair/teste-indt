namespace PropostaService.Application.Ports;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken ct)
        where T : class;
}
