using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {

        public string FullName { get; set; }

        public string? Address { get; set; }

        public string? Country { get; set; }

        [InverseProperty(nameof(UserRefreshToken.user))]
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();

    }
}
