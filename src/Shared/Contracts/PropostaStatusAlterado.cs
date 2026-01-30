namespace Shared.Contracts;

public record PropostaStatusAlterado(
    Guid PropostaId,
    int NovoStatus,
    DateTime OccurredAtUtc
);
