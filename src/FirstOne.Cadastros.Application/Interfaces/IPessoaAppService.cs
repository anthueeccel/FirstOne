using FirstOne.Cadastros.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IPessoaAppService
    {
        IEnumerable<PessoaViewModel> GetAll();
        Task AddAsync(PessoaViewModel pessoa);
        Task UpdateAsync(PessoaViewModel pessoa);
        Task RemoveAsync(Guid id);
        PessoaViewModel GetById(Guid id);
    }
}
