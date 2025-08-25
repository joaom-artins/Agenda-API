using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.Application.Services.Interfaces.v1;

public interface IUserService
{
    Task<IEnumerable<UserGetAllResponse>> GetAll();
    Task<IEnumerable<UserGetProfessionalsResponse>> GetProfessionals();
}
