using EmployeeManagement.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        IAssignmentRepository Assignments { get; }
        Task CompleteAsync();
    }
}
