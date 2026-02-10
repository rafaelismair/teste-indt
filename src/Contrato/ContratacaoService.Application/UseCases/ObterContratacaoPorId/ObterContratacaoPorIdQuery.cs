using ContratacaoService.Application.DTOs;
using MediatR;

namespace ContratacaoService.Application.UseCases.ObterContratacaoPorId;

public sealed record ObterContratacaoPorIdQuery(Guid Id) : IRequest<ContratacaoResponse?>;
