using FirstOne.Cadastros.Domain.Validations;
using System;

namespace FirstOne.Cadastros.Domain.Commands
{
    public class RemovePessoaCommand : Command
    {
        public Guid Id { get; }

        public RemovePessoaCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePessoaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }    
    }
}
