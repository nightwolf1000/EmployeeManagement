using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

namespace EmployeeManagement.Utitlity
{
    public class RequestLocalizationCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLocalizationCustomMiddleware(RequestDelegate next) => _next = next;
       
        public async Task InvokeAsync(HttpContext context)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

            await _next(context);
        }
    }
}
