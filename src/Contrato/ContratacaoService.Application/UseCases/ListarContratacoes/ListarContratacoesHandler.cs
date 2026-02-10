using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Ports;
using MediatR;

namespace ContratacaoService.Application.UseCases.ListarContratacoes;

public sealed class ListarContratacoesHandler : IRequestHandler<ListarContratacoesQuery, IReadOnlyList<ContratacaoResponse>>
{
    private readonly IContratacaoReadRepository _repo;

    public ListarContratacoesHandler(IContratacaoReadRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<ContratacaoResponse>> Handle(ListarContratacoesQuery request, CancellationToken ct)
    {
        var all = await _repo.GetAllAsync(ct);
        return all
            .Select(x => new ContratacaoResponse(x.Id, x.PropostaId, x.DataContratacaoUtc))
            .ToList();
    }
}
