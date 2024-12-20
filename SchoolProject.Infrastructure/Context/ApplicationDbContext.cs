using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentSubject>()
                            .HasKey(x => new { x.SubjectId, x.DepartmentId });

            modelBuilder.Entity<InstructorSubject>()
                        .HasKey(x => new { x.SubjectId, x.InstructorId });

            modelBuilder.Entity<StudentSubject>()
                        .HasKey(x => new { x.SubjectId, x.StudentId });

            modelBuilder.Entity<Instructor>()
                        .HasOne(x => x.Supervisor)
                        .WithMany(x => x.Instructors)
                        .HasForeignKey(x => x.SupervisorId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
            .HasOne(x => x.Instructor)
            .WithOne(x => x.departmentManager)
            .HasForeignKey<Department>(x => x.InsManger)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
