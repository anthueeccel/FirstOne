using FirstOne.Cadastros.Domain.Enums;
using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class UsuarioPermissao : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        public EntidadeEnum EntidadeEnum { get; private set; }
        public string EndPoint { get; private set; }

        public UsuarioPermissao(Guid id, Guid usuarioId, EntidadeEnum entidade, string endPoint)
        {
            Id = id;
            UsuarioId = usuarioId;
            EntidadeEnum = entidade;
            EndPoint = endPoint;
        }

        public UsuarioPermissao() { }
    }
}
