using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Configuration
{
    [DbConfigurationAttribute(typeof(ApplicationDbContext))]
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.NameAr).HasMaxLength(500);
            entity.HasMany(x => x.Students)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Instructor)
                .WithOne(x => x.departmentManager)
                .HasForeignKey<Department>(x => x.InsManger)
                .OnDelete(DeleteBehavior.Restrict);

            entity
          .HasOne(x => x.Instructor)
          .WithOne(x => x.departmentManager)
          .HasForeignKey<Department>(x => x.InsManger)
          .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
