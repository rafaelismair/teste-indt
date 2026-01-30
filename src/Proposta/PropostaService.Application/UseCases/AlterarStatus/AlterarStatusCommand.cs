using MediatR;
using PropostaService.Domain.Propostas;

namespace PropostaService.Application.UseCases.AlterarStatus;

public record AlterarStatusCommand(
    Guid PropostaId,
    PropostaStatus NovoStatus
) : IRequest;
