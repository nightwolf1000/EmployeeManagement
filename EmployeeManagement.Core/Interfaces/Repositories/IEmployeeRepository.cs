using EmployeeManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {        
        Task<IEnumerable<Employee>> GetEmployees(string searchTerm = null, params Expression<Func<Employee, object>>[] includes);

        Task<IEnumerable<Employee>> GetEmployeesByAssignmentId(int assignmentId);

        Task<Employee> GetEmployee(int employeeId, params Expression<Func<Employee, object>>[] includes);    
        
        Task AddEmployee(Employee employee);        

        void Remove(Employee employee);
    }
}
