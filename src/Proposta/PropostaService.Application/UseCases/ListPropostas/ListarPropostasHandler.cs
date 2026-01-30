using MediatR;
using PropostaService.Application.Ports;
using PropostaService.Domain.Propostas;

namespace PropostaService.Application.UseCases.ListPropostas;

public class ListarPropostasHandler : IRequestHandler<ListPropostasQuery, IReadOnlyList<Proposta>>
{
    private readonly IPropostaRepository _repository;

    public ListarPropostasHandler(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Proposta>> Handle(ListPropostasQuery request, CancellationToken ct)
        => _repository.ListAsync(ct);
}
