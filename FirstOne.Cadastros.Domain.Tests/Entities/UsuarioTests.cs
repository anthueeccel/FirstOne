using FirstOne.Cadastros.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.Entities
{
    public class UsuarioTests
    {
        [Fact(DisplayName = "Verifica entidade Usuário")]
        [Trait("Cadastro", "Usuário")]
        public void deve_criar_entidade_pessoa()
        {
            var usuarioEsperado = new { Id = Guid.NewGuid(), Email = "teste@teste", Senha = "1234", PessoaId = Guid.NewGuid(), Role = "motorista" };

            var usuario = new Usuario(usuarioEsperado.Id, usuarioEsperado.Email, usuarioEsperado.Senha, usuarioEsperado.PessoaId, usuarioEsperado.Role);
            usuarioEsperado.Should().Equals(usuario);
            usuario.Id.Should().Be(usuarioEsperado.Id);
            usuario.Email.Should().Be(usuarioEsperado.Email);
            usuario.Senha.Should().Be(usuarioEsperado.Senha);
            usuario.PessoaId.Should().Be(usuarioEsperado.PessoaId);
        }
    }
}
