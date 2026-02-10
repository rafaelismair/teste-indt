using ContratacaoService.Application.DTOs;
using MediatR;

namespace ContratacaoService.Application.UseCases.ListarContratacoes;

public sealed record ListarContratacoesQuery : IRequest<IReadOnlyList<ContratacaoResponse>>;
