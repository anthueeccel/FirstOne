using FirstOne.Cadastros.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> GetAll();
        Task AddAsync(PessoaViewModel pessoa);
        Task UpdateAsync(PessoaViewModel pessoa);
    }
}
