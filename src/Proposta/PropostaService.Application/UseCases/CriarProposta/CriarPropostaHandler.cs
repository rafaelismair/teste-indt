using MediatR;
using PropostaService.Application.Ports;
using PropostaService.Domain.Propostas;

namespace PropostaService.Application.UseCases.CriarProposta;

public class CriarPropostaHandler : IRequestHandler<CriarPropostaCommand, Guid>
{
    private readonly IPropostaRepository _repository;

    public CriarPropostaHandler(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CriarPropostaCommand request, CancellationToken ct)
    {
        var proposta = new Proposta(
            request.NomeSegurado,
            request.ValorCobertura
        );

        await _repository.AddAsync(proposta, ct);
        await _repository.SaveChangesAsync(ct);

        return proposta.Id;
    }
}
