using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Service.Commons.LoggedUser.Interfaces;

namespace Service.Commons.LoggedUser;

public class GetLoggedUser(
    IHttpContextAccessor _httpContextAccessor
) : IGetLoggedUser
{
    public Guid GetId() => Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("userId")!);
}
