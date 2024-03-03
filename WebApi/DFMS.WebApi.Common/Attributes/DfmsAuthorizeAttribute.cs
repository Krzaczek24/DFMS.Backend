using DFMS.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.WebApi.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class DfmsAuthorizeAttribute : AuthorizeAttribute
    {
        private const char SEPARATOR = ',';

        private IReadOnlyCollection<UserRole> roles = new List<UserRole>();
        public new IReadOnlyCollection<UserRole> Roles
        {
            get => roles;
            set {
                roles = value;
                base.Roles = string.Join(SEPARATOR, roles.Select(r => r.ToString().ToUpper()));
            }
        }

        public DfmsAuthorizeAttribute(params UserRole[] roles)
        {
            Roles = roles;
        }
    }
}
