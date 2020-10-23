using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
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

        public IEnumerable<UsuarioPermissao> GetPermissoes(Guid usuarioId)
        {
            return _context.UsuarioPermissao
                .AsNoTracking()
                .Where(x => x.UsuarioId == usuarioId)
                .ToList();
        }

        public void AdicionarPermissao(Guid usuarioId, EntidadeEnum rotina, string valor)
        {
            var permissaoDb = _context.UsuarioPermissao.AsNoTracking().Where(x => x.Id == usuarioId && x.EntidadeEnum == rotina).FirstOrDefault();

            if (permissaoDb == null)
            {

                var permissaoUsuario = new UsuarioPermissao(Guid.NewGuid(), usuarioId, rotina, valor);
                _context.UsuarioPermissao.Add(permissaoUsuario);
            }
            else
            {
                var permissaoUsuario = new UsuarioPermissao(Guid.NewGuid(), usuarioId, rotina, valor);
                _context.UsuarioPermissao.Update(permissaoUsuario);
            }
            _context.SaveChanges();
        }
    }
}
