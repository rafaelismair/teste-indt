using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropostaService.Api.Contracts.Propostas;
using PropostaService.Application.UseCases.AlterarStatus;
using PropostaService.Application.UseCases.CriarProposta;
using PropostaService.Application.UseCases.GetPropostaById;
using PropostaService.Application.UseCases.ListPropostas;
using PropostaService.Domain.Exceptions;
using PropostaService.Domain.Propostas;

namespace PropostaService.Api.Controllers;

[ApiController]
[Route("propostas")]
public class PropostasController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropostasController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CriarPropostaRequest request, CancellationToken ct)
    {
        if (request is null) return BadRequest(Problem("Body inválido."));
        if (string.IsNullOrWhiteSpace(request.NomeSegurado)) return BadRequest(Problem("NomeSegurado é obrigatório."));
        if (request.ValorCobertura <= 0) return BadRequest(Problem("ValorCobertura deve ser > 0."));

        var id = await _mediator.Send(new CriarPropostaCommand(request.NomeSegurado, request.ValorCobertura), ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PropostaResumoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var propostas = await _mediator.Send(new ListPropostasQuery(), ct);

        var result = propostas.Select(p => new PropostaResumoResponse(
            p.Id,
            p.NomeSegurado,
            p.ValorCobertura,
            p.Status.ToString(),
            p.CreatedAtUtc
        )).ToList();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PropostaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var proposta = await _mediator.Send(new GetPropostaByIdQuery(id), ct);
        if (proposta is null) return NotFound(Problem("Proposta não encontrada."));

        var result = new PropostaResponse(
            proposta.Id,
            proposta.NomeSegurado,
            proposta.ValorCobertura,
            proposta.Status.ToString(),
            proposta.CreatedAtUtc
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}/status")]
    [ProducesResponseType(typeof(PropostaStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStatus(Guid id, CancellationToken ct)
    {
        var proposta = await _mediator.Send(new GetPropostaByIdQuery(id), ct);
        if (proposta is null) return NotFound(Problem("Proposta não encontrada."));

        return Ok(new PropostaStatusDto(proposta.Id, (int)proposta.Status));
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AlterarStatus(Guid id, [FromBody] AlterarStatusRequest request, CancellationToken ct)
    {
        if (request is null) return BadRequest(Problem("Body inválido."));
        if (string.IsNullOrWhiteSpace(request.NovoStatus)) return BadRequest(Problem("NovoStatus é obrigatório."));

        if (!TryParseStatus(request.NovoStatus, out var novoStatus))
            return BadRequest(Problem("NovoStatus inválido. Use: EmAnalise, Aprovada, Rejeitada."));

        try
        {
            await _mediator.Send(new AlterarStatusCommand(id, novoStatus), ct);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(Problem("Proposta não encontrada."));
        }
        catch (DomainException ex)
        {
            return Conflict(Problem(ex.Message));
        }
    }

    private static bool TryParseStatus(string input, out PropostaStatus status)
    {
        input = input.Trim();

        if (string.Equals(input, "Em Análise", StringComparison.OrdinalIgnoreCase)) input = "EmAnalise";
        if (string.Equals(input, "EmAnalise", StringComparison.OrdinalIgnoreCase)) { status = PropostaStatus.EmAnalise; return true; }
        if (string.Equals(input, "Aprovada", StringComparison.OrdinalIgnoreCase)) { status = PropostaStatus.Aprovada; return true; }
        if (string.Equals(input, "Rejeitada", StringComparison.OrdinalIgnoreCase)) { status = PropostaStatus.Rejeitada; return true; }

        status = default;
        return false;
    }
}
