using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Core.Interfaces.Repositories;
using EmployeeManagement.Persistence.Repositories;
using System.Threading.Tasks;

namespace EmployeeManagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository Employees { get; }
        public IDepartmentRepository Departments { get; }
        public IAssignmentRepository Assignments { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Departments = new DepartmentRepository(_context);
            Assignments = new AssignmentRepository(_context);
        }        

        public async Task CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();        
    }
}
