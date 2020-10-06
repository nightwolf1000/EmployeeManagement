using EmployeeManagement.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.EntityConfigurations
{
    class EmployeeAssignmentConfiguration : IEntityTypeConfiguration<EmployeeAssignment>
    {
        public void Configure(EntityTypeBuilder<EmployeeAssignment> builder)
        {
            builder.HasKey(ea => new { ea.EmployeeId, ea.AssignmentId });
        }
    }
}
