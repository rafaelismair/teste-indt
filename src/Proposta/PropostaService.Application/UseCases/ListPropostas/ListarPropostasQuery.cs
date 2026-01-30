using MediatR;
using PropostaService.Domain.Propostas;

namespace PropostaService.Application.UseCases.ListPropostas;

public record ListPropostasQuery : IRequest<IReadOnlyList<Proposta>>;
