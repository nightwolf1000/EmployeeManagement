using EmployeeManagement.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.EntityConfigurations
{
    class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(p => p.ImageData)
                .IsRequired();
                
            builder.ToTable("Photos");
        }
    }
}
