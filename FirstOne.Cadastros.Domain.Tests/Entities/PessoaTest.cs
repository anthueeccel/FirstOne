using FirstOne.Cadastros.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests
{
    public class PessoaTest
    {
        [Fact(DisplayName = "Verifica entidade Pessoa ")]
        [Trait("Cadastro", "Pessoa")]
        public void deve_criar_entidade_pessoa()
        {
            var pessoaEsperada = new { Id = Guid.NewGuid(), Nome = "Teste" };

            var pessoa = new Pessoa(pessoaEsperada.Id, pessoaEsperada.Nome);
            pessoaEsperada.Should().Equals(pessoa);
            pessoa.Id.Should().Be(pessoaEsperada.Id);
            pessoa.Nome.Should().Be(pessoaEsperada.Nome);
        }
    }
}
