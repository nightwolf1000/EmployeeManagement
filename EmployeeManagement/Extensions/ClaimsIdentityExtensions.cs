using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EmployeeManagement.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static List<string> Roles(this ClaimsIdentity identity)
        {
            return identity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }
    }
}
