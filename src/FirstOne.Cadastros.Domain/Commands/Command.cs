﻿using FluentValidation.Results;
using MediatR;

namespace FirstOne.Cadastros.Domain.Commands
{
    public abstract class Command : IRequest<Unit>
    {
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
