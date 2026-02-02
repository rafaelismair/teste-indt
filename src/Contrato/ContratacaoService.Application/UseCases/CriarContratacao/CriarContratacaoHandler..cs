using ContratacaoService.Application.Ports;
using ContratacaoService.Domain.Contratacao;
using MediatR;

namespace ContratacaoService.Application.UseCases.CriarContratacao;

public sealed class CriarContratacaoHandler : IRequestHandler<CriarContratacaoCommand, Guid>
{
    private readonly IContratacaoRepository _repo;

    public CriarContratacaoHandler(IContratacaoRepository repo)
        => _repo = repo;

    public async Task<Guid> Handle(CriarContratacaoCommand request, CancellationToken ct)
    {
        if (await _repo.ExistsByPropostaIdAsync(request.PropostaId, ct))
            return Guid.Empty; 

        var contratacao = new Contratacao(request.PropostaId, DateTime.UtcNow);

        await _repo.AddAsync(contratacao, ct);
        await _repo.SaveChangesAsync(ct);

        return contratacao.Id;
    }
}
