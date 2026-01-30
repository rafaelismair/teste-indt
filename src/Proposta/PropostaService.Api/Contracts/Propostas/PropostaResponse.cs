namespace PropostaService.Api.Contracts.Propostas;

public record PropostaResponse(
    Guid Id,
    string NomeSegurado,
    decimal ValorCobertura,
    string Status,
    DateTime CreatedAtUtc
);
