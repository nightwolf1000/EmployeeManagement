using EmployeeManagement.Extensions;
using EmployeeManagement.Utitlity.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Utitlity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomApiAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly List<string> _roles;       

        public CustomApiAuthorizeAttribute(params string[] roles) => _roles = roles.ToList();

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (HasAllowAnonymous(context))
                return Task.CompletedTask;

            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                throw new UnauthorizedCustomException();

            if (_roles.Any() && !_roles.Intersect((user.Identity as ClaimsIdentity).Roles()).Any())
                throw new ForbiddenCustomException();

            return Task.CompletedTask;
        }

        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            return context.ActionDescriptor
                .EndpointMetadata
                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        }        
    }
}
