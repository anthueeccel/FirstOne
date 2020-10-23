using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GetAll();
        IEnumerable<Usuario> Search(Expression<Func<Usuario, bool>> predicate);
        void AdicionarPermissao(Guid userId, EntidadeEnum rotina, string valor);
        IEnumerable<UsuarioPermissao> GetPermissoes(Guid usuarioId);
    }
}
