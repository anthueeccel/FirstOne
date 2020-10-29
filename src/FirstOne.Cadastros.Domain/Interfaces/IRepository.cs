namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
