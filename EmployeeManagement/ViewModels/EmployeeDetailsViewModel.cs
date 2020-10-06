using EmployeeManagement.Core.Domain;
using static Constants;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public string ViewHeading => ViewHeadings.EmployeeDetails;
    }
}
