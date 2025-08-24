using Service.Data.Context;
using Service.Data.Repositories.Generic;
using Service.Data.Repositories.Interfaces.v1.User;
using Service.Domain.Models.v1;

namespace Service.Data.Repositories.v1.User;

public class UserRepository(
    AppDbContext context
) : GenericRepository<UserModel>(context),
    IUserRepository
{
}
