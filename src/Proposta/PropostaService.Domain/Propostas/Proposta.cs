using PropostaService.Domain.Exceptions;

namespace PropostaService.Domain.Propostas;

public class Proposta
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string NomeSegurado { get; private set; } = default!;
    public decimal ValorCobertura { get; private set; }
    public PropostaStatus Status { get; private set; } = PropostaStatus.EmAnalise;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    private Proposta() { }

    public Proposta(string nomeSegurado, decimal valorCobertura)
    {
        if (string.IsNullOrWhiteSpace(nomeSegurado))
            throw new DomainExceptionImpl("Nome do segurado é obrigatório.");

        if (valorCobertura <= 0)
            throw new DomainExceptionImpl("Valor de cobertura deve ser maior que zero.");

        NomeSegurado = nomeSegurado.Trim();
        ValorCobertura = valorCobertura;
    }

    public void AlterarStatus(PropostaStatus novoStatus)
    {
        if (Status != PropostaStatus.EmAnalise)
            throw new PropostaStatusInvalidoException($"Não é permitido alterar status quando a proposta está {Status}.");

        if (novoStatus == PropostaStatus.EmAnalise)
            throw new PropostaStatusInvalidoException("Novo status inválido.");

        Status = novoStatus;
    }

    private sealed class DomainExceptionImpl : DomainException
    {
        public DomainExceptionImpl(string message) : base(message) { }
    }
}
