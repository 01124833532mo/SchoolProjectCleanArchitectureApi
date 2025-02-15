using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Configuration
{
    [DbConfigurationAttribute(typeof(ApplicationDbContext))]

    public class InstractorSubjectConfiguration : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> entity)
        {
            entity.HasOne(ds => ds.instructor)
                           .WithMany(d => d.InstructorSubjects)
                           .HasForeignKey(ds => ds.InstructorId);

            entity.HasOne(ds => ds.Subject)
                .WithMany(d => d.InstructorSubjects)
                .HasForeignKey(ds => ds.SubjectId);




            entity.HasKey(x => new { x.SubjectId, x.InstructorId });


        }
    }
}
