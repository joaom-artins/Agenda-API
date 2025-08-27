using Microsoft.EntityFrameworkCore;
using Service.Data.Context;
using Service.Data.Repositories.Generic.Interfaces;

namespace Service.Data.Repositories.Generic;

public class GenericRepository<T>(
    AppDbContext _context
): IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
    public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().SingleOrDefaultAsync(e => EF.Property<Guid>(e, "id") == id);
    public async Task<T?> GetByUserIdAsync(Guid userId) => await _context.Set<T>().SingleOrDefaultAsync(e => EF.Property<Guid>(e, "userId") == userId);
    public async Task AddAsync(T t) => await _context.Set<T>().AddAsync(t);
    public void Update(T t) => _context.Set<T>().Update(t);
    public void Delete(T t) => _context.Set<T>().Remove(t);
}
