using ContratacaoService.Application.UseCases.ListarContratacoes;
using ContratacaoService.Application.UseCases.ObterContratacaoPorId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoService.Api.Controllers;

[ApiController]
[Route("api/contratacoes")]
public sealed class ContratacoesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContratacoesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new ListarContratacoesQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterContratacaoPorIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }
}
