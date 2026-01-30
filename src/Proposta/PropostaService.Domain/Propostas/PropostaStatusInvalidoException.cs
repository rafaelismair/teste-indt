using PropostaService.Domain.Exceptions;

namespace PropostaService.Domain.Propostas;

public sealed class PropostaStatusInvalidoException : DomainException
{
    public PropostaStatusInvalidoException(string message) : base(message) { }
}
