using DFMS.Database.Dto;
using DFMS.Shared.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using System.Data;

namespace DFMS.WebApi.Authorization
{
    public class TokenBuilder
    {
        private SymmetricSecurityKey Key { get; }
        private List<Claim> UserClaims { get; }

        public TokenBuilder(string key, User user)
        {
            string userName = $"{user.FirstName} {user.LastName}".Trim();

            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            UserClaims = new List<Claim>
            {
                new CustomClaim(ClaimTypes.Name, string.IsNullOrEmpty(userName) ? user.Login : userName),
                new CustomClaim(ClaimTypes.Role, user.Role),
                new CustomClaim(UserClaim.Id, user.Id.ToString()),
                new CustomClaim(UserClaim.Login, user.Login),
                new CustomClaim(UserClaim.Role, user.Role),
                new CustomClaim(UserClaim.FirstName, user.FirstName ?? string.Empty),
                new CustomClaim(UserClaim.LastName, user.LastName ?? string.Empty),
            };

            if (user.Privileges?.Length > 0)
            {
                UserClaims.AddRange(user.Privileges.Select(privilege => new CustomClaim(UserClaim.Permissions, privilege)));
            }
        }

        public string GetToken()
        {
            var userData = UserClaims.Append(new CustomClaim(UserClaim.LastLoginDate, DateTime.Now.ToString(StringFormats.Dates.DateTime)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(userData),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return token;
        }
    }
}
