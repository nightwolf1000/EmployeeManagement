using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManagement.Core.Domain
{
    public class Assignment
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public ICollection<EmployeeAssignment> Employees { get; set; }
        public Assignment() => Employees = new Collection<EmployeeAssignment>();

        public int GetEmployeeNumber() => Employees.Count;        
    }
}
