using EmployeeManagement.Core.Domain;
using System.Collections.Generic;
using static Constants;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeListViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public string ViewHeading => ViewHeadings.Employees;
        public string SearchTerm { get; set; }
    }
}
