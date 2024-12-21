using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Configuration
{
    [DbConfigurationAttribute(typeof(ApplicationDbContext))]

    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> entity)
        {
            entity.HasOne(ds => ds.Department)
                               .WithMany(d => d.DepartmentSubjects)
                               .HasForeignKey(ds => ds.DepartmentId);

            entity.HasOne(ds => ds.Subject)
                .WithMany(d => d.DepartmetsSubjects)
                .HasForeignKey(ds => ds.SubjectId);


            entity.HasKey(x => new { x.SubjectId, x.DepartmentId });

        }
    }
}
