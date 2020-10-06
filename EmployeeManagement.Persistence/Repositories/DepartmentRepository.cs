using EmployeeManagement.Core.Domain;
using EmployeeManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) => _context = context;        
        
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }
    }
}
