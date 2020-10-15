using FirstOne.Cadastros.Domain.Validations;
using FluentValidation.Results;
using MediatR;
using System;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class UpdatePessoaCommand : Command
    {
        public Guid Id { get; }
        public string Nome { get; }

        public UpdatePessoaCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
