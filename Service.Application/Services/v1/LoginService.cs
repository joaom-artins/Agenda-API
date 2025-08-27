using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Service.Commons.Notification;
using Service.Commons;
using Service.Domain.Models.v1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Service.Commons.Notification.Interface;
using Service.Domain.Dtos.Request.v1.Login;
using Service.Data.Repositories.Interfaces.v1;
using Service.Domain.Dtos.Responses.v1.Users;
using Service.Data.Repositories.v1;
using System.Security.Cryptography;
using Service.Data.UnitOfWork.Interfaces;

namespace Service.Application.Services.v1;

public class LoginService(
    AppSettings _appSettings,
    IUnitOfWork _unitOfWork,
    SignInManager<UserModel> _signInManager,
    IUserRepository _userRepository,
    IUserRefreshTokenRepository _userRefreshTokenRepository,
    INotificationContext _notificationContext,
    UserManager<UserModel> _userManager
)
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var userRecord = await _userRepository.GetByEmailOrPhoneNumberAsync(request.EmailOrPhoneNumber);
        if (userRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return default!;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(userRecord, request.Password, false);
        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.InvalidData
            );
            return default!;
        }

        var accessToken = await GenerateAccessTokenAsync(userRecord);
        if (_notificationContext.HasNotifications)
        {
            return default!;
        }

        var refreshToken = await GenerateRefreshTokenWithoutValidationAsync(userRecord.Id);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.Expiration),
        };
    }

    public async Task<string> GenerateRefreshTokenWithoutValidationAsync(Guid userId)
    {
        byte[] randomNumber = new byte[64];
        var refreshToken = "";
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            refreshToken = Convert.ToBase64String(randomNumber);
        }

        var record = await _userRefreshTokenRepository.GetByUserIdAsync(userId);
        if (record is not null)
        {
            _userRefreshTokenRepository.Delete(record);
        }

        var userRefreshTokenModel = new UserRefreshTokenModel
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.RefreshTokenExpiration)
        };

        await _userRefreshTokenRepository.AddAsync(userRefreshTokenModel);
        await _unitOfWork.CommitAsync();

        return refreshToken;
    }

    private async Task<string> GenerateAccessTokenAsync(UserModel user)
    {
        var role = await _userManager.GetRolesAsync(user);
        if (role is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status401Unauthorized,
                title: NotificationTitle.Unauthorized,
                detail: NotificationMessage.User.InvalidData
            );
            return default!;
        }

        var tokenHandle = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new("userId", user!.Id.ToString()),
            new(ClaimTypes.Role, role.First()),
        };

        var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.SecretKey);

        var token = tokenHandle.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.Expiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandle.WriteToken(token);
    }
}
