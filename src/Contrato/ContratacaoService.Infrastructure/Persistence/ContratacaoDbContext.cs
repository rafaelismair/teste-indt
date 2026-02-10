using ContratacaoService.Domain.Contratacao;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoService.Infrastructure.Persistence;

public sealed class ContratacaoDbContext : DbContext
{
    public ContratacaoDbContext(DbContextOptions<ContratacaoDbContext> options) : base(options) { }

    public DbSet<Contratacao> Contratacoes => Set<Contratacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contratacao>(entity =>
        {
            entity.ToTable("contratacoes");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.PropostaId)
                  .IsRequired();

            entity.Property(x => x.DataContratacaoUtc)
                .IsRequired();

            entity.HasIndex(x => x.PropostaId)
                  .IsUnique();

        });
    }
}
