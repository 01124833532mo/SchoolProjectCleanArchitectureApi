using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService(IOptions<JwtSettings> jwtsettings) : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings = jwtsettings.Value;

        public Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>()
                 {
        new Claim(nameof(UserClaimModel.UserName), user.UserName),
        new Claim(nameof(UserClaimModel.Email), user.Email),
        new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                };

            var jwtTokentObj = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtTokentObj);

            return Task.FromResult(accessToken);

        }
    }
}
