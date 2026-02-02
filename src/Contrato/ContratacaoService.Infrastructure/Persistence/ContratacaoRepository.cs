using ContratacaoService.Application.Ports;
using ContratacaoService.Domain.Contratacao;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoService.Infrastructure.Persistence;

public sealed class ContratacaoRepository : IContratacaoRepository
{
    private readonly ContratacaoDbContext _db;

    public ContratacaoRepository(ContratacaoDbContext db) => _db = db;

    public Task<bool> ExistsByPropostaIdAsync(Guid propostaId, CancellationToken ct) =>
        _db.Contratacoes.AnyAsync(x => x.PropostaId == propostaId, ct);

    public Task AddAsync(Contratacao contratacao, CancellationToken ct) =>
        _db.Contratacoes.AddAsync(contratacao, ct).AsTask();

    public Task SaveChangesAsync(CancellationToken ct) =>
        _db.SaveChangesAsync(ct);
}
