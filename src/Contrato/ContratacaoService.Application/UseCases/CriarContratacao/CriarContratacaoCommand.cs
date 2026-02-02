using MediatR;

namespace ContratacaoService.Application.UseCases.CriarContratacao;

public sealed record CriarContratacaoCommand(Guid PropostaId) : IRequest<Guid>;
