using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Configuration
{
    [DbConfigurationAttribute(typeof(ApplicationDbContext))]

    internal class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {

        public void Configure(EntityTypeBuilder<StudentSubject> entity)
        {
            entity.HasOne(ds => ds.Student)
                              .WithMany(d => d.StudentSubjects)
                              .HasForeignKey(ds => ds.SubjectId);

            entity.HasOne(ds => ds.Subject)
                .WithMany(d => d.StudentsSubjects)
                .HasForeignKey(ds => ds.SubjectId);

            entity
              .HasKey(x => new { x.SubjectId, x.StudentId });
        }
    }
}
