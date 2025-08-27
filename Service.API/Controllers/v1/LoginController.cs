using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Application.Services.Interfaces.v1;
using Service.Application.Services.v1;
using Service.Commons.Notification.Interface;
using Service.Domain.Dtos.Request.v1.Login;
using Service.Domain.Dtos.Responses.v1.Users;

namespace Service.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v1/login")]
public class LoginController(
    INotificationContext _notificationContext,
    IWebHostEnvironment _environment,
    ILoginService _loginService
) : ControllerBase
{
    /// <summary>
    /// ANONYMOUS: Loga um usuário
    /// </summary>
    /// <response code="200">Retorna sucesso da operação</response>
    /// <response code="400">Retorna erro de requisição inválida</response>
    /// <response code="401">Retorna erro de não autorizado</response>
    /// <response code="500">Retorna erro interno do servidor</response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginService.LoginAsync(request);

        if (!_notificationContext.HasNotifications)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(1)
            };
            if (_environment.IsProduction())
            {
                cookieOptions.Domain = ".agenda.com.br";
            }
            Response.Cookies.Append("AUTH_TOKEN", result.AccessToken, cookieOptions);
        }

        return Ok(result);
    }
}
