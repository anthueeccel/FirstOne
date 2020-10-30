using FirstOne.Cadastros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GetAll();
        IEnumerable<Usuario> Search(Expression<Func<Usuario, bool>> predicate);
        void AdicionarClaim(UsuarioClaim usuarioClaim);
        void RemoverClaims(UsuarioClaim claim);
    }
}
