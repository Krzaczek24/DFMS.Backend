using DFMS.Shared.Extensions;
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
        public static string GetRole(this ClaimsPrincipal claims) => claims.Get(UserClaim.Role);
        public static IReadOnlyCollection<string> GetPermissions(this ClaimsPrincipal claims) => claims.GetMany(UserClaim.Permissions);
        public static bool HasPermission(this ClaimsPrincipal claims, string permission) => GetPermissions(claims).Contains(permission);
        public static bool HasAnyPermissions(this ClaimsPrincipal claims, IEnumerable<string> permissions) => GetPermissions(claims).HasAny(permissions);
        public static bool HasAllPermissions(this ClaimsPrincipal claims, IEnumerable<string> permissions) => GetPermissions(claims).HasAll(permissions);

        #region BASE FUNCTIONS
        public static string Get(this ClaimsPrincipal claims, UserClaim userClaim) => Get(claims, userClaim.ToCamelCase());

        private static string Get(this ClaimsPrincipal claims, string userClaim)
        {
            try { return claims.Claims.SingleOrDefault(claim => claim.Type == userClaim).Value; }
            catch { throw new KeyNotFoundException($"Found more than one value for [{userClaim}] key in user claims set"); }
        }

        public static T Get<T>(this ClaimsPrincipal claims, UserClaim userClaim)
        {
            string value = claims.Get(userClaim);
            try { return (T)Convert.ChangeType(value, typeof(T)); }
            catch { throw new InvalidCastException($"Cannot convert user [{userClaim}] claim value [{value}] from [{value.GetType().Name}] to [{typeof(T).Name}] type"); }
        }

        public static IReadOnlyCollection<string> GetMany(this ClaimsPrincipal claims, UserClaim userClaim)
            => GetMany(claims, userClaim.ToCamelCase()).ToList().AsReadOnly();

        private static IEnumerable<string> GetMany(this ClaimsPrincipal claims, string userClaim)
            => claims.Claims.Where(claim => claim.Type == userClaim).Select(claim => claim.Value);

        public static IReadOnlyCollection<T> GetMany<T>(this ClaimsPrincipal claims, UserClaim userClaim)
        {
            try { return claims.GetMany(userClaim.ToCamelCase()).Cast<T>().ToList().AsReadOnly(); }
            catch { throw new InvalidCastException($"Cannot convert user [{userClaim}] claim value collection from [{typeof(string).Name}] to [{typeof(T).Name}] type"); }
        }
        #endregion BASE FUNCTIONS
    }
}