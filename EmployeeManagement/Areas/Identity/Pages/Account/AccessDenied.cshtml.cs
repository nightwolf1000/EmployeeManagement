using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace EmployeeManagement.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public void OnGet() => Response.StatusCode = (int)HttpStatusCode.Forbidden;
    }
}

