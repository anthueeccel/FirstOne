using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        public IEnumerable<Pessoa> GetAll()
        {
            return new List<Pessoa>()
            {
                new Pessoa(Guid.NewGuid(), "Anthue"),
                new Pessoa(Guid.NewGuid(), "Teste")
            };
        }
    }
}
