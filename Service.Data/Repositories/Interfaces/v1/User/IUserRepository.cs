using Microsoft.EntityFrameworkCore;
using Service.Data.Repositories.Generic.Interfaces;
using Service.Domain.Models.v1;

namespace Service.Data.Repositories.Interfaces.v1.User;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<IEnumerable<UserModel>> GetProfessionalsAsync();
    Task<IEnumerable<UserModel>> GetByEmailOrPhoneNumberAsync(string email, string phoneNumber);
}
