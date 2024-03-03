using DFMS.Database.Dto.Users;
using DFMS.Shared.Constants;
using DFMS.Shared.Extensions;
using DFMS.WebApi.Common.Enums;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DFMS.WebApi.Authorization.Token
{
    public class TokenBuilder
    {
        private const string DATE_FORMAT = StringFormats.Dates.ISO_8601;

        private SymmetricSecurityKey Key { get; }

        public TokenBuilder(string key)
        {
            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        public string GenerateAccessToken(UserDto user)
        {
            DateTime now = DateTime.UtcNow;

            var userClaims = new List<Claim>
            {
                new CustomClaim(UserClaim.Id, user.Id.ToString()),
                new CustomClaim(UserClaim.Login, user.Login),
                new CustomClaim(UserClaim.Name, user.Name),
                new CustomClaim(UserClaim.Role, user.Role.ToString().ToUpper()),
                new CustomClaim(UserClaim.FirstName, user.FirstName ?? string.Empty),
                new CustomClaim(UserClaim.LastName, user.LastName ?? string.Empty),
                new CustomClaim(UserClaim.CreatedAt, user.CreatedAt!.Value.ToString(DATE_FORMAT)),
                new CustomClaim(UserClaim.LastLoginDate, user.LastLogin?.ToString(DATE_FORMAT) ?? string.Empty),
            };

            if (user.Permissions?.Length > 0)
            {
                userClaims.AddRange(user.Permissions.Select(permission => new CustomClaim(UserClaim.Permissions, permission)));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(userClaims, null, UserClaim.Name.ToCamelCase(), UserClaim.Role.ToCamelCase()),
                IssuedAt = now,
#if DEBUG
                Expires = now.Add(TimeSpan.FromDays(42)),
#else
                Expires = now.Add(TimeSpan.FromMinutes(5)),
#endif
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken(out DateTime? validUntil)
        {
            DateTime now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                IssuedAt = now,
                Expires = validUntil = now.Add(TimeSpan.FromDays(7)),
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public static bool IsRefreshTokenValid(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(refreshToken))
                return false;

            var tokenData = tokenHandler.ReadJwtToken(refreshToken);

            return tokenData.ValidTo > DateTime.UtcNow;
        }
    }
}
