using System.Security.Cryptography;

namespace Service.Data.Repositories.Generic.Interfaces;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T t);
    void Update(T t);
    void Delete(T t);
}
