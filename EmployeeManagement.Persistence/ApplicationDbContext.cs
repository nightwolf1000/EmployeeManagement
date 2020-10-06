using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Domain;
using EmployeeManagement.Persistence.EntityConfigurations;
using EmployeeManagement.Persistence.Extensions;

namespace EmployeeManagement.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<EmployeeAssignment> EmployeeAssignments { get; set; }        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new AssignmentConfiguration());
            builder.ApplyConfiguration(new EmployeeAssignmentConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration());            
            builder.ConfigureDbFunctions();
        }       
    }
}
