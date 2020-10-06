using EmployeeManagement.Core.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Employee> ApplySearchOfEmployees(this IQueryable<Employee> query, string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
                return query;           

            query = query.Where(e =>
                            e.Name.Contains(searchTerm) ||
                            e.Department.Name.Contains(searchTerm) ||
                            e.Position.Contains(searchTerm) ||
                            e.HireDate.ToCustomString().Contains(searchTerm)); 
            return query;            
        }

        public static IQueryable<Assignment> ApplySearchOfAssignments(this IQueryable<Assignment> query, string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
                return query;

            return query.Where(a => a.Name.Contains(searchTerm));            
        }

        public static IQueryable<T> IncludeRelateData<T>(this IQueryable<T> query, Expression<Func<T, object>>[] includes) where T : class
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
