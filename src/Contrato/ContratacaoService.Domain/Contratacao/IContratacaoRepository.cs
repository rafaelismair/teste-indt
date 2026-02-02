using ContratacaoService.Domain.Contratacao;

namespace ContratacaoService.Application.Ports;

public interface IContratacaoRepository
{
    Task<bool> ExistsByPropostaIdAsync(Guid propostaId, CancellationToken ct);
    Task AddAsync(Contratacao contratacao, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
