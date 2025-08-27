using Microsoft.EntityFrameworkCore;
using Service.Data.Repositories.Generic.Interfaces;
using Service.Domain.Models.v1;

namespace Service.Data.Repositories.Interfaces.v1;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<IEnumerable<UserModel>> GetProfessionalsAsync();
    Task<IEnumerable<UserModel>> FindByEmailOrPhoneNumberAsync(string email, string phoneNumber);
    Task<UserModel?> GetByEmailOrPhoneNumberAsync(string emailOrPhoneNumber);
}
