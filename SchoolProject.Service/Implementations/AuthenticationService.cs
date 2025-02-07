using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstractions;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService/*(IOptions<JwtSettings> jwtsettings)*/ : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings /*= jwtsettings.Value*/;
        private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(IOptions<JwtSettings> jwtsettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtsettings.Value;
            _UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GeneratJWTToken(user);

            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreskToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult();
            response.RefreshToken = refreshToken;
            response.AccessToken = accessToken;
            return response;


        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshtoken = new RefreshToken()
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()

            };
            _UserRefreshToken.AddOrUpdate(refreshtoken.TokenString, refreshtoken, (s, t) => refreshtoken);

            return refreshtoken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
                 {
                   //new Claim(ClaimTypes.Role, "Admin"),
                   new Claim(ClaimTypes.NameIdentifier,user.UserName),
                   new Claim(ClaimTypes.Email,user.Email),
                     new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                     new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),



                };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userclaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userclaims);

            //new Claim(nameof(UserClaimModel.UserName), user.UserName),
            //      new Claim(nameof(UserClaimModel.Email), user.Email),
            //    new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),

            return claims;
        }

        private async Task<(JwtSecurityToken, string)> GeneratJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtTokent = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtTokent);

            return (jwtTokent, accessToken);
        }

        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expirydate, string RefreshToken)
        {

            var (jwtSecurityToken, newtoken) = await GeneratJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newtoken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = RefreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expirydate;
            response.RefreshToken = refreshTokenResult;
            return response;




        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ValidateToken(string AccessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime
            };
            try
            {
                var validator = handler.ValidateToken(AccessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "invalidToken";
                }
                return "Not Expired ";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmisWrong", null);
            }

            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("tokenisnotExpired", null);

            }
            // Get User ID from JWT Token
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id))?.Value;


            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                             .FirstOrDefaultAsync(x => x.Token == AccessToken &&
                            x.RefreskToken == RefreshToken &&
                            x.UserId == int.Parse(userId));
            // Check if Refresh Token is found
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            // Check if Refresh Token is expired
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }
    }
}
