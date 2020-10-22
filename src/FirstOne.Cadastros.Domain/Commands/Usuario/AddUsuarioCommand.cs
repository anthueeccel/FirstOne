using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Validations.Usuario;
using System;

namespace FirstOne.Cadastros.Domain.Commands.Usuario
{
    public class AddUsuarioCommand : Command
    {
        public string Email { get; }
        public string Senha { get; }
        public Guid PessoaId { get; }


        public AddUsuarioCommand(string email, string senha, Guid pessoaId)
        {
            Email = email;
            Senha = senha;
            PessoaId = pessoaId;
        }

        public bool IsValid()
        {
            ValidationResult = new AddUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
