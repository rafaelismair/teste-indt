using Microsoft.EntityFrameworkCore;
using PropostaService.Domain.Propostas;

namespace PropostaService.Infrastructure.Persistence;

public class PropostaDbContext : DbContext
{
    public PropostaDbContext(DbContextOptions<PropostaDbContext> options) : base(options) { }

    public DbSet<Proposta> Propostas => Set<Proposta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proposta>(b =>
        {
            b.ToTable("propostas");
            b.HasKey(x => x.Id);

            b.Property(x => x.NomeSegurado)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.ValorCobertura)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            b.Property(x => x.Status)
                .IsRequired();

            b.Property(x => x.CreatedAtUtc)
                .IsRequired();

            b.ToTable("propostas");
            b.HasKey(x => x.Id);

        });
    }
}
