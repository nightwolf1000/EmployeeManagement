using EmployeeManagement.Core.Domain;
using System.Collections.Generic;
using static Constants;

namespace EmployeeManagement.ViewModels
{
    public class AssignmentListViewModel
    {
        public IEnumerable<Assignment> Assignments { get; set; }
        public string ViewHeading => ViewHeadings.Assignments;
        public string SearchTerm { get; set; }
    }
}
