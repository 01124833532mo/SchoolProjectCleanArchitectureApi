using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Common;
using System.Reflection;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<UserRefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Department>().Property(p => p.NameAr).HasMaxLength(50);




            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InformationAssembly).Assembly,
                type => type.GetCustomAttribute<DbConfigurationAttribute>()?.Dbcontext == (typeof(ApplicationDbContext)));



            base.OnModelCreating(modelBuilder);
        }
    }
}
