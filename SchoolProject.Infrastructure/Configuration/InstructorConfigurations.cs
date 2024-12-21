using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Configuration
{
    [DbConfigurationAttribute(typeof(ApplicationDbContext))]

    internal class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {

            builder.HasOne(x => x.Supervisor)
            .WithMany(x => x.Instructors)
            .HasForeignKey(x => x.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
