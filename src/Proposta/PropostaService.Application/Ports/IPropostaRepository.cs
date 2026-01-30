using PropostaService.Domain.Propostas;

namespace PropostaService.Application.Ports;

public interface IPropostaRepository
{
    Task AddAsync(Proposta proposta, CancellationToken ct);
    Task<Proposta?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Proposta>> ListAsync(CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
