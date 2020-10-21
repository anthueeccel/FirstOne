using FirstOne.Cadastros.Domain.Entities;
using System.Collections.Generic;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GetAll();
    }
}
