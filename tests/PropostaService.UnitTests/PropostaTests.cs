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

    [Fact]
    public void Nao_deve_permitir_nome_segurado_vazio()
    {
        Action act = () => new Proposta(string.Empty, 1000);
        act.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Nao_deve_permitir_valor_cobertura_zero_ou_negativo(decimal valor)
    {
        Action act = () => new Proposta("João", valor);
        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Nao_deve_alterar_status_para_em_analise()
    {
        var proposta = new Proposta("João", 1000);
        Action act = () => proposta.AlterarStatus(PropostaStatus.EmAnalise);
        act.Should().Throw<Exception>();
    }
}