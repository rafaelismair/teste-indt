using MediatR;
using PropostaService.Application.Ports;
using Shared.Contracts;


namespace PropostaService.Application.UseCases.AlterarStatus;

public class AlterarStatusHandler : IRequestHandler<AlterarStatusCommand, Unit>
{
    private readonly IPropostaRepository _repository;
    private readonly IEventBus _eventBus;

    public AlterarStatusHandler(IPropostaRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(AlterarStatusCommand request, CancellationToken ct)
    {
        var proposta = await _repository.GetByIdAsync(request.PropostaId, ct)
            ?? throw new KeyNotFoundException("Proposta não encontrada.");

        proposta.AlterarStatus(request.NovoStatus);

        await _repository.SaveChangesAsync(ct);

        var evento = new PropostaStatusAlterado(
            proposta.Id,
            (int)proposta.Status,
            DateTime.UtcNow
        );

        await _eventBus.PublishAsync(evento, ct);

        return Unit.Value;
    }
}
