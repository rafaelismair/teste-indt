using ContratacaoService.Domain.Contratacao;
using FluentAssertions;

namespace ContratacaoService.UnitTests;

public class ContratacaoTests
{
    [Fact]
    public void Deve_criar_contratacao_valida()
    {
        var propostaId = Guid.NewGuid();
        var data = DateTime.UtcNow;
        var contratacao = new Contratacao(propostaId, data);
        contratacao.PropostaId.Should().Be(propostaId);
        contratacao.DataContratacaoUtc.Should().Be(data);
    }

    [Fact]
    public void Deve_lancar_excecao_se_propostaid_vazio()
    {
        Action act = () => new Contratacao(Guid.Empty, DateTime.UtcNow);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Deve_definir_data_contratacao_corretamente()
    {
        var propostaId = Guid.NewGuid();
        var data = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var contratacao = new Contratacao(propostaId, data);
        contratacao.DataContratacaoUtc.Should().Be(data);
    }

    [Fact]
    public void Id_deve_ser_diferente_de_guid_empty()
    {
        var contratacao = new Contratacao(Guid.NewGuid(), DateTime.UtcNow);
        contratacao.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Deve_permitir_multiplas_contratacoes_com_diferentes_propostaid()
    {
        var c1 = new Contratacao(Guid.NewGuid(), DateTime.UtcNow);
        var c2 = new Contratacao(Guid.NewGuid(), DateTime.UtcNow);
        c1.PropostaId.Should().NotBe(c2.PropostaId);
    }
}
