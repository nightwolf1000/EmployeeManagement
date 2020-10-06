using Microsoft.AspNetCore.Builder;

namespace EmployeeManagement.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder HandleErrorResponse(this IApplicationBuilder builder)
        {
            // returns Api error response
            builder.UseWhen(context => context.Request.IsAjaxRequest(), appBuilder =>
            {
                appBuilder.UseStatusCodePagesWithReExecute("/api/errors/{0}");
                appBuilder.UseExceptionHandler("/api/errors");
            });
            // returns MVC error page
            builder.UseWhen(context => !context.Request.IsAjaxRequest(), appBuilder =>
            {
                appBuilder.UseStatusCodePagesWithReExecute("/errors/{0}");
                appBuilder.UseExceptionHandler("/errors");
            });

            return builder;
        }       
    }
}
