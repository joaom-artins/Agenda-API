using Service.Domain.Dtos.Request.v1.Login;
using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.Application.Services.Interfaces.v1;

public interface ILoginService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
