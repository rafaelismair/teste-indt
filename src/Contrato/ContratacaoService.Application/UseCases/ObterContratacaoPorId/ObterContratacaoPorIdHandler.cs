using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Ports;
using MediatR;

namespace ContratacaoService.Application.UseCases.ObterContratacaoPorId;

public sealed class ObterContratacaoPorIdHandler : IRequestHandler<ObterContratacaoPorIdQuery, ContratacaoResponse?>
{
    private readonly IContratacaoReadRepository _repo;

    public ObterContratacaoPorIdHandler(IContratacaoReadRepository repo) => _repo = repo;

    public async Task<ContratacaoResponse?> Handle(ObterContratacaoPorIdQuery request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id, ct);
        if (entity is null) return null;

        return new ContratacaoResponse(entity.Id, entity.PropostaId, entity.DataContratacaoUtc);
    }
}
