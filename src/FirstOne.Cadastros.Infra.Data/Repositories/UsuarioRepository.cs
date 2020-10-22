using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FirstOne.Cadastros.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private SqlServerContext _context;

        public UsuarioRepository(SqlServerContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuario.AsNoTracking()
                .Include(x => x.Pessoa)
                .ToList();
        }

        public IEnumerable<Usuario> Search(Expression<Func<Usuario, bool>> predicate)
        {
            return _context.Usuario
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }
    }
}
