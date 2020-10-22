using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class PessoaPermissoes : EntidadeBase
    {
        public Guid UserId { get; private set; }
        public Usuario Usuario { get; private set; }

        public Rotinas Rotinas { get; private set; }
        public string Valor { get; private set; }

        public PessoaPermissoes(Guid userId, Usuario usuario, Rotinas rotinas, string permissao)
        {
            UserId = userId;
            Usuario = usuario;
            Rotinas = rotinas;
            Valor = permissao;
        }

        public PessoaPermissoes() { }
    }
}
