using MediatR;

namespace PropostaService.Application.UseCases.CriarProposta;

public record CriarPropostaCommand(
    string NomeSegurado,
    decimal ValorCobertura
) : IRequest<Guid>;
