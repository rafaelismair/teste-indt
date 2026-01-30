namespace PropostaService.Api.Contracts.Propostas;

public record PropostaResumoResponse(
    Guid Id,
    string NomeSegurado,
    decimal ValorCobertura,
    string Status,
    DateTime CreatedAtUtc
);
