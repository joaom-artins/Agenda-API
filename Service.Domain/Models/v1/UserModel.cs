using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Service.Domain.Enums.v1.User;

namespace Service.Domain.Models.v1
{
    public class UserModel : IdentityUser<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Occupation { get; set; } = string.Empty;
        public UserTypeEnum Type { get; set; }

    }
}
