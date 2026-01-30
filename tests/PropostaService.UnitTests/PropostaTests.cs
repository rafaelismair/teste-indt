using FluentAssertions;
using PropostaService.Domain.Propostas;

namespace PropostaService.UnitTests;

public class PropostaTests
{
    [Fact]
    public void Deve_iniciar_em_analise()
    {
        var proposta = new Proposta("João", 1000);
        proposta.Status.Should().Be(PropostaStatus.EmAnalise);
    }

    [Fact]
    public void Deve_aprovar_quando_em_analise()
    {
        var proposta = new Proposta("João", 1000);
        proposta.AlterarStatus(PropostaStatus.Aprovada);
        proposta.Status.Should().Be(PropostaStatus.Aprovada);
    }

    [Fact]
    public void Nao_deve_alterar_status_se_ja_finalizada()
    {
        var proposta = new Proposta("João", 1000);
        proposta.AlterarStatus(PropostaStatus.Aprovada);

        Action act = () => proposta.AlterarStatus(PropostaStatus.Rejeitada);
        act.Should().Throw<Exception>();
    }
}
