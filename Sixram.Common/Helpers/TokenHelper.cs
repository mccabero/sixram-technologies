using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sixram.DTO;
using Sixram.Models;
using Sixram.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sixram.Common.Helpers
{
    public static class TokenHelper
    {
        public static List<string> GetUserRoles(HttpContext context)
        {
            var claims = context.User.Claims;
            var roles = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roles == null)
                return new List<string>();

            var userRoles = JsonConvert.DeserializeObject<List<string>>(roles.Value);

            return userRoles;
        }

        public static int GetCurrentUserId(HttpContext context)
        {
            var claims = context.User.Claims;

            if (claims.Any())
            {
                var id = claims.Where(x => x.Type == "Id");

                return (id != null && id.Any())
                    ? int.Parse(id.FirstOrDefault()!.Value)
                    : 0;
            }

            return 0;
        }

        public static bool IsTokenIsValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken == null) || (jwtToken.ValidTo > DateTime.Now);
        }

        public static async Task<LoginResponseModel> GenerateClaimsResponse(UsersDto model, IOptions<AppSettings> _appSettings)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.Secret));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claims = [];

                claims = GenerateUserClaims(model);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _appSettings.Value.ApiIssuer,
                    audience: _appSettings.Value.ApiUrl,
                    claims: claims,
                    expires: DateTime.Now.AddDays(_appSettings.Value.TokenExpirationInDays),

                    signingCredentials: signinCredentials
                );

                var tokenValue = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                var response = new LoginResponseModel(
                    isAuthenticated: true,
                    token: tokenValue,
                    errorMessage: null);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static List<Claim> GenerateUserClaims(UsersDto? user)
        {
            var userClaims = new List<Claim>()
                {
                    new Claim("Id", user.Id.ToString()  ?? string.Empty),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"  ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email.ToString()  ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Roles.Name.ToString())
                };

            if (user.UserRoles == null)
                return userClaims;

            foreach (var u in user.UserRoles)
                userClaims.Add(new Claim(ClaimTypes.Role, u.Roles.Name));

            return userClaims;
        }
    }
}