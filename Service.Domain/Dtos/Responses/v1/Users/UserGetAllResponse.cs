using Service.Domain.Enums.v1;

namespace Service.Domain.Dtos.Responses.v1.Users;

public class UserGetAllResponse
{
    public string Name { get; set; } = string.Empty;
    public string? Occupation { get; set; }
    public UserTypeEnum Type { get; set; }
}
