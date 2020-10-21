using FirstOne.Cadastros.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        IEnumerable<UsuarioViewModel> GetAll();
        Task AddAsync(UsuarioViewModel usuarioViewModel);
    }
}
