using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public class Usuario : EntidadeBase
    {
        public string Email { get; }
        public string Senha { get; }
        public Guid PessoaId { get; }

        public Usuario(Guid id, string email, string senha, Guid pessoaId)
        {
            Id = id;
            Email = email;
            Senha = senha;
            PessoaId = pessoaId;
        }
    }
}
