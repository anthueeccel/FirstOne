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
    public class UsuarioRepository : Repository, IUsuarioRepository
    {
        public UsuarioRepository(SqlServerContext context) : base(context) { }

        public void Add(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuario
                .AsNoTracking()
                .Include(x => x.Pessoa)
                .Include(x => x.UsuarioClaims)
                .ToList();
        }

        public IEnumerable<Usuario> Search(Expression<Func<Usuario, bool>> predicate)
        {
            return _context.Usuario
                .AsNoTracking()
                .Include(x => x.UsuarioClaims)
                .Where(predicate)
                .ToList();
        }

        public void AdicionarClaim(UsuarioClaim usuarioClaim)
        {
            _context.UsuarioClaim.Add(usuarioClaim);
        }

        public void RemoverClaims(UsuarioClaim usuarioClaim)
        {
            _context.UsuarioClaim.Remove(usuarioClaim);
        }
    }
}
