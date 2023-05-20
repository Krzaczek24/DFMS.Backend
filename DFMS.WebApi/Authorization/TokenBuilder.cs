using DFMS.Shared.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using System.Data;
using DFMS.Database.Dto.Users;
using DFMS.Shared.Extensions;
using Microsoft.VisualBasic;

namespace DFMS.WebApi.Authorization
{
    public class TokenBuilder
    {
        private const string DATE_FORMAT = StringFormats.Dates.ISO_8601;

        private SymmetricSecurityKey Key { get; }
        private List<Claim> UserClaims { get; }

        public TokenBuilder(string key, User user)
        {
            string userName = $"{user.FirstName} {user.LastName}".Trim();

            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            UserClaims = new List<Claim>
            {
                new CustomClaim(UserClaim.Id, user.Id.ToString()),
                new CustomClaim(UserClaim.Login, user.Login),
                new CustomClaim(UserClaim.Name, string.IsNullOrEmpty(userName) ? user.Login : userName),
                new CustomClaim(UserClaim.Role, user.Role),
                new CustomClaim(UserClaim.FirstName, user.FirstName ?? string.Empty),
                new CustomClaim(UserClaim.LastName, user.LastName ?? string.Empty),
                new CustomClaim(UserClaim.CreatedAt, user.CreatedAt!.Value.ToString(DATE_FORMAT)),
                new CustomClaim(UserClaim.LastLoginDate, user.LastLogin?.ToString(DATE_FORMAT) ?? string.Empty),
            };

            if (user.Permissions?.Length > 0)
            {
                UserClaims.AddRange(user.Permissions.Select(permission => new CustomClaim(UserClaim.Permissions, permission)));
            }
        }

        public string GenerateToken(TimeSpan? tokenLifeTime = null)
        {
            DateTime now = DateTime.UtcNow;
            tokenLifeTime ??= TimeSpan.FromHours(1);

            var userData = UserClaims.Append(new CustomClaim(UserClaim.CurrentLoginDate, now.ToString(DATE_FORMAT)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(userData, null, UserClaim.Name.ToCamelCase(), UserClaim.Role.ToCamelCase()),
                IssuedAt = now,
                Expires = now.Add(tokenLifeTime.Value),
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
