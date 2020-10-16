using FirstOne.Cadastros.Domain.Entities;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        IEnumerable<Pessoa> GetAll();
        void Add(Pessoa pessoa);
        void Update(Pessoa pessoa);
    }
}
