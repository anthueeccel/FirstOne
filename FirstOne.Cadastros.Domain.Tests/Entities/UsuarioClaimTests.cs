using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.Entities
{
    public class UsuarioClaimTests
    {

        [Fact(DisplayName = "Verifica entidade Usuário Claim")]
        [Trait("Categoria", "Usuário")]
        public void deve_instanciar_entidade_usuario_claim()
        {
            //Arrange
            var esperado = new
            {
                Id = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                Entidade = EntidadeEnum.Usuario,
                Endpoint = "Add, update, Remove"
            };

            //Act
            var atual = new UsuarioClaim(esperado.Id, esperado.UsuarioId, esperado.Entidade, esperado.Endpoint);

            //Asert
            esperado.Should().Equals(atual);
        }
    }
}
