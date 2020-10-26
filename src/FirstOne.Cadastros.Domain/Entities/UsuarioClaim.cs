using FirstOne.Cadastros.Domain.Enums;
using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class UsuarioClaim : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        public EntidadeEnum EntidadeEnum { get; private set; }
        public string Endpoint { get; private set; }

        public UsuarioClaim(Guid id, Guid usuarioId, EntidadeEnum entidade, string endpoint)
        {
            Id = id;
            UsuarioId = usuarioId;
            EntidadeEnum = entidade;
            Endpoint = endpoint;
        }

        public UsuarioClaim() { }
    }
}
