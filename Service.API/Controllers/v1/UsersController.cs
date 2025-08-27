using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Application.Services.Interfaces.v1;
using Service.Domain.Dtos.Request.v1.Users;
using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v1/users")]
public class UsersController(
    IUserService _userService
) : ControllerBase
{
    /// <summary>
    /// ANONYMOUS: Cria um usuário
    /// </summary>
    /// <response code="204">Retorna sucesso da operação</response>
    /// <response code="400">Retorna erro de requisição inválida</response>
    /// <response code="500">Retorna erro interno do servidor</response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
    {
        await _userService.CreateAsync(request);

        return NoContent();
    }
}
