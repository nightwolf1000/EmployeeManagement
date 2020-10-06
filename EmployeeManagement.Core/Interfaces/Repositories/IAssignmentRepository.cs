using EmployeeManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Interfaces.Repositories
{
    public interface IAssignmentRepository
    {        
        Task<IEnumerable<Assignment>> GetAssignments(string searchTerm = null, params Expression<Func<Assignment, object>>[] includes);

        Task<IEnumerable<Assignment>> GetAssignmentsByEmployeeId(int employeeId);

        Task<Assignment> GetAssignment(int assignmentId, params Expression<Func<Assignment, object>>[] includes); 
        
        Task AddAssignment(Assignment assignment);

        void Remove(Assignment assignment);
    }
}
