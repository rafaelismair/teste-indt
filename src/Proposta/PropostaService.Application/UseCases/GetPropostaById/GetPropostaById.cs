using MediatR;
using PropostaService.Application.Ports;
using PropostaService.Domain.Propostas;

namespace PropostaService.Application.UseCases.GetPropostaById;

public class GetPropostaByIdHandler : IRequestHandler<GetPropostaByIdQuery, Proposta?>
{
    private readonly IPropostaRepository _repository;

    public GetPropostaByIdHandler(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public Task<Proposta?> Handle(GetPropostaByIdQuery request, CancellationToken ct)
        => _repository.GetByIdAsync(request.Id, ct);
}
