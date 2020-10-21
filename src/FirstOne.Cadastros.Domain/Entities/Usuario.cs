using System;
using System.Diagnostics;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class Usuario : EntidadeBase
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Guid PessoaId { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }
        
        public Usuario(Guid id, string email, string senha, Guid pessoaId)
        {
            Id = id;
            Email = email;
            Senha = senha;
            PessoaId = pessoaId;            
        }

        protected Usuario() { }
    }
}
