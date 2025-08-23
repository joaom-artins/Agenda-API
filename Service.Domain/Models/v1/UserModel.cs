using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Service.Domain.Models.v1
{
    public class UserModel : IdentityUser<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Occupation { get; set; } = string.Empty;

    }
}
