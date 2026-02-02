using ContratacaoService.Application.UseCases.CriarContratacao;
using MassTransit;
using MediatR;
using Shared.Contracts;
using System;

namespace ContratacaoService.Infrastructure.Messaging.Consumers;

public sealed class PropostaStatusAlteradoConsumer : IConsumer<PropostaStatusAlterado>
{
    private readonly IMediator _mediator;

    public PropostaStatusAlteradoConsumer(IMediator mediator)
        => _mediator = mediator;

    public async Task Consume(ConsumeContext<PropostaStatusAlterado> context)
    {
        if (context.Message.NovoStatus != 1)
            return;

        await _mediator.Send(new CriarContratacaoCommand(context.Message.PropostaId), context.CancellationToken);
    }
}
