using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Application.Services.Interfaces.v1;
using Service.Domain.Dtos.Request.v1.Users;

namespace Service.API.Controllers.v1;

[ApiController]
[Route("v1/users")]
public class UsersController(
    IUserService _userService
) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
    {
        await _userService.CreateAsync(request);

        return NoContent();
    }
}
