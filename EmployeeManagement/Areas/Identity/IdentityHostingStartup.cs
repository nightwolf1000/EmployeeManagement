using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EmployeeManagement.Areas.Identity.IdentityHostingStartup))]
namespace EmployeeManagement.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}