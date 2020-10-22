using System;

namespace FirstOne.Cadastros.Domain.Entities
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; protected set; }
    }
}
