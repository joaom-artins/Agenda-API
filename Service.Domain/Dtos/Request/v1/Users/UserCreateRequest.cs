using Service.Domain.Enums.v1;

namespace Service.Domain.Dtos.Request.v1.Users;

public class UserCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Occupation { get; set; }
    public UserTypeEnum Type { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
