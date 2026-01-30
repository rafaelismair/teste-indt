using MediatR;
using PropostaService.Domain.Propostas;

namespace PropostaService.Application.UseCases.GetPropostaById;

public record GetPropostaByIdQuery(Guid Id) : IRequest<Proposta?>;
