namespace Service.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    void BeginTransaction();
    Task CommitAsync(bool isCloseTransaction = false);
}
