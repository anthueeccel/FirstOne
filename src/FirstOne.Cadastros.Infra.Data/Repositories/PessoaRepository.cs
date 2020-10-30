using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public class PessoaRepository : Repository, IPessoaRepository
    {
        public PessoaRepository(SqlServerContext context) : base(context) { }
        
        public void Add(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _context.Pessoa.AsNoTracking().ToList();
        }

        public Pessoa GetById(Guid id)
        {
            return _context.Pessoa.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Pessoa pessoa)
        {
            _context.Pessoa.Update(pessoa);
        }
    }
}
