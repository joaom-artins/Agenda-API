using Service.Domain.Dtos.Request.v1.Users;
using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.Application.Services.Interfaces.v1;

public interface IUserService
{
    Task<IEnumerable<UserGetAllResponse>> GetAllAsync();
    Task<IEnumerable<UserGetProfessionalsResponse>> GetProfessionalsAsync();
    Task<bool> CreateAsync(UserCreateRequest request);
}
