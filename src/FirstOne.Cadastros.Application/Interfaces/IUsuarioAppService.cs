using FirstOne.Cadastros.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        string Login(string email, string senha);
        IEnumerable<UsuarioViewModel> GetAll();
        Task AddAsync(UsuarioViewModel usuarioViewModel);
    }
}
