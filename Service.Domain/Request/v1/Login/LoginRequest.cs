namespace Service.Domain.Request.v1.Login
{
    public class LoginRequest
    {
        public string EmailOrPhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
