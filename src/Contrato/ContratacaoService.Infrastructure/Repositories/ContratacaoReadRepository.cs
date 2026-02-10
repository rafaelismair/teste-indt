using ContratacaoService.Application.Ports;
using ContratacaoService.Domain.Contratacao;
using ContratacaoService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoService.Infrastructure.Repositories;

public sealed class ContratacaoReadRepository : IContratacaoReadRepository
{
    private readonly ContratacaoDbContext _db;

    public ContratacaoReadRepository(ContratacaoDbContext db) => _db = db;

    public Task<Contratacao?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _db.Contratacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Contratacao>> GetAllAsync(CancellationToken ct) =>
        await _db.Contratacoes.AsNoTracking().OrderByDescending(x => x.DataContratacaoUtc).ToListAsync(ct);
}
