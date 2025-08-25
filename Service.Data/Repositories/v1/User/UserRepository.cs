using Microsoft.EntityFrameworkCore;
using Service.Data.Context;
using Service.Data.Repositories.Generic;
using Service.Data.Repositories.Interfaces.v1.User;
using Service.Domain.Enums.v1.User;
using Service.Domain.Models.v1;

namespace Service.Data.Repositories.v1.User;

public class UserRepository(
    AppDbContext context
) : GenericRepository<UserModel>(context),
    IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<UserModel>> GetProfessionals() => await _context.Users.Where(e => e.Type == UserTypeEnum.Professional).ToListAsync();
}
