using Service.Data.Context;
using Service.Data.UnitOfWork.Interfaces;

namespace Service.Data.UnitOfWork;

public class UnitOfWork(
    AppDbContext _context
) : IUnitOfWork
{
    public void BeginTransaction()
    {
        _context.Database.BeginTransaction();
    }

    public async Task CommitAsync(bool isCloseTransaction = false)
    {
        await _context.SaveChangesAsync();
        if (isCloseTransaction)
        {
            _context.Database.CurrentTransaction?.Commit();
        }
    }
}
