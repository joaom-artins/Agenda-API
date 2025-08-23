using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Service.Domain.Models
{
    public class UserModel : IdentityUser<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
    }
}
