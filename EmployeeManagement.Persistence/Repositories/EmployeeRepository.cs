using EmployeeManagement.Core.Domain;
using EmployeeManagement.Core.Interfaces.Repositories;
using EmployeeManagement.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagement.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) => _context = context;        

        public async Task<IEnumerable<Employee>> GetEmployees(string searchTerm = null, params Expression<Func<Employee, object>>[] includes)
        {
            IQueryable<Employee> query = _context.Employees;

            if (includes != null)
                query = query.IncludeRelateData(includes);

            return await query.ApplySearchOfEmployees(searchTerm)
                .OrderBy(e => e.Department.Name)
                .ThenBy(e => e.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByAssignmentId(int assignmentId)
        {
            return await _context.EmployeeAssignments
                .Include(ea => ea.Employee)
                    .ThenInclude(e => e.Department)
                .Where(ea => ea.AssignmentId == assignmentId)
                .Select(ea => ea.Employee)
                .OrderBy(e => e.Department.Name)
                .ThenBy(e => e.Name)
                .AsNoTracking()
                .ToListAsync();
        }       

        public async Task<Employee> GetEmployee(int employeeId, params Expression<Func<Employee, object>>[] includes)
        {
            IQueryable<Employee> query = _context.Employees;

            if (includes != null)
                query = query.IncludeRelateData(includes);

            return await query.SingleOrDefaultAsync(e => e.Id == employeeId);
        }        

        public async Task AddEmployee(Employee employee) => await _context.Employees.AddAsync(employee);

        public void Remove(Employee employee) => _context.Employees.Remove(employee);         
    }
}
