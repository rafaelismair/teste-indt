namespace ContratacaoService.Application.DTOs;

public sealed record ContratacaoResponse(
    Guid Id,
    Guid PropostaId,
    DateTime DataContratacaoUtc
);
