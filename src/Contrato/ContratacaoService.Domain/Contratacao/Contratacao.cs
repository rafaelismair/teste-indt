namespace ContratacaoService.Domain.Contratacao;

public sealed class Contratacao
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PropostaId { get; private set; }
    public DateTime DataContratacaoUtc { get; private set; }

    private Contratacao() { } 

    public Contratacao(Guid propostaId, DateTime dataContratacaoUtc)
    {
        if (propostaId == Guid.Empty)
            throw new ArgumentException("PropostaId inválido.", nameof(propostaId));

        PropostaId = propostaId;
        DataContratacaoUtc = dataContratacaoUtc;
    }
}
