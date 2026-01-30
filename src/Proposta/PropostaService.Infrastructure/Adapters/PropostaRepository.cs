using Microsoft.EntityFrameworkCore;
using PropostaService.Application.Ports;
using PropostaService.Domain.Propostas;
using PropostaService.Infrastructure.Persistence;

namespace PropostaService.Infrastructure.Adapters;

public class PropostaRepository : IPropostaRepository
{
    private readonly PropostaDbContext _db;

    public PropostaRepository(PropostaDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Proposta proposta, CancellationToken ct)
        => await _db.Propostas.AddAsync(proposta, ct);

    public Task<Proposta?> GetByIdAsync(Guid id, CancellationToken ct)
        => _db.Propostas.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IReadOnlyList<Proposta>> ListAsync(CancellationToken ct)
        => await _db.Propostas
            .OrderByDescending(p => p.CreatedAtUtc)
            .ToListAsync(ct);

    public Task SaveChangesAsync(CancellationToken ct)
        => _db.SaveChangesAsync(ct);
}
