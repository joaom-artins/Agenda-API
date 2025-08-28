using Microsoft.EntityFrameworkCore;
using Service.Data.Context;
using Service.Data.Repositories.Generic;
using Service.Data.Repositories.Interfaces.v1;
using Service.Domain.Enums.v1;
using Service.Domain.Models.v1;

namespace Service.Data.Repositories.v1;

public class UserRepository(
    AppDbContext context
) : GenericRepository<UserModel>(context),
    IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<UserModel>> GetProfessionalsAsync() => await _context.Users.AsNoTracking().Where(e => e.Type == UserTypeEnum.Professional).ToListAsync();
    public async Task<IEnumerable<UserModel>> FindByEmailOrPhoneNumberAsync(string email, string phoneNumber) => await _context.Users.AsNoTracking().Where(x => x.Email == email || x.PhoneNumber == phoneNumber).ToListAsync();
    public async Task<UserModel?> GetByEmailOrPhoneNumberAsync(string emailOrPhoneNumber) => await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == emailOrPhoneNumber || x.PhoneNumber == emailOrPhoneNumber);

}
