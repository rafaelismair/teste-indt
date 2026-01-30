using MassTransit;
using Shared.Contracts;

namespace ContratacaoService.Infrastructure.Messaging.Consumers;

public sealed class PropostaStatusAlteradoConsumer : IConsumer<PropostaStatusAlterado>
{
    public Task Consume(ConsumeContext<PropostaStatusAlterado> context)
    {
        Console.WriteLine(
            $"[ContratacaoService] Evento recebido: PropostaId={context.Message.PropostaId} NovoStatus={context.Message.NovoStatus} Data={context.Message.OccurredAtUtc:o}");

        return Task.CompletedTask;
    }
}
