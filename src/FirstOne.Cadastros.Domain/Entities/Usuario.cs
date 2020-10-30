using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class Usuario : EntidadeBase
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Guid PessoaId { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }
        public string Role { get; private set; }

        private readonly List<UsuarioClaim> _usuarioClaims;
        public IReadOnlyCollection<UsuarioClaim> UsuarioClaims => _usuarioClaims;


        public Usuario(Guid id, string email, string senha, Guid pessoaId, string role)
        {
            Id = id;
            Email = email;
            Senha = senha;
            PessoaId = pessoaId;
            Role = role;
            _usuarioClaims = new List<UsuarioClaim>();
        }

        public void AdicionarClaim(UsuarioClaim usuarioClaim)
        {
            _usuarioClaims.Add(usuarioClaim);
        }

        protected Usuario() { }       
    }
}
