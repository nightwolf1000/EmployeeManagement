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
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AssignmentRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Assignment>> GetAssignments()
        {
            return await _context.Assignments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignments(string searchTerm = null, params Expression<Func<Assignment, object>>[] includes)
        {
            IQueryable<Assignment> query = _context.Assignments;

            if (includes != null)
                query = query.IncludeRelateData(includes);

            return await query.ApplySearchOfAssignments(searchTerm)
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByEmployeeId(int employeeId)
        {
            return await _context.EmployeeAssignments
                .Include(ea => ea.Assignment)
                .Where(ea => ea.EmployeeId == employeeId)
                .Select(ea => ea.Assignment)
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .ToListAsync();
        }        

        public async Task<Assignment> GetAssignment(int assignmentId, params Expression<Func<Assignment, object>>[] includes)
        {
            IQueryable<Assignment> query = _context.Assignments;

            if (includes != null)
                query = query.IncludeRelateData(includes);

            return await query.SingleOrDefaultAsync(a => a.Id == assignmentId);
        }

        public async Task AddAssignment(Assignment assignment) => await _context.Assignments.AddAsync(assignment);

        public void Remove(Assignment assignment) => _context.Assignments.Remove(assignment);        
    }
}
