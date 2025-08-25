using Service.Domain.Dtos.Request.v1.Users;
using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.Application.Services.Interfaces.v1;

public interface IUserService
{
    Task<IEnumerable<UserGetAllResponse>> GetAll();
    Task<IEnumerable<UserGetProfessionalsResponse>> GetProfessionals();
    Task<bool> CreateAsync(UserCreateRequest request);
}
