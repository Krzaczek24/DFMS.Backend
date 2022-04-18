using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DFMS.WebApi.Authorization
{
    public static class UserExtension
    {
        public static int GetID(this ClaimsPrincipal claims) => claims.Get<int>(UserClaim.Id);
        public static string GetLogin(this ClaimsPrincipal claims) => claims.Get(UserClaim.Login);

        public static string Get(this ClaimsPrincipal claims, UserClaim userClaim)
        {
            try
            {
                return claims.Claims.Where(claim => claim.Type == userClaim.ToString()).Single().Value;
            }
            catch
            {
                throw new KeyNotFoundException($"The [{userClaim}] key has been not found in user claims set");
            }
        }

        public static T Get<T>(this ClaimsPrincipal claims, UserClaim userClaim)
        {
            string value = claims.Get(userClaim);
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                throw new InvalidCastException($"Cannot convert user claim value [{value}] from [{value.GetType().Name}] to [{typeof(T).Name}] type");
            }
        }
    }
}
