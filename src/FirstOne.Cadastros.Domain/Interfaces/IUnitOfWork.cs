using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
