using ContratacaoService.Domain.Contratacao;


namespace ContratacaoService.Application.Ports;

public interface IContratacaoReadRepository
{
    Task<Contratacao?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Contratacao>> GetAllAsync(CancellationToken ct);
}
