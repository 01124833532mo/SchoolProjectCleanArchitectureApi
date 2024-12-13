using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository  : GenericRepository<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _students;

        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _students = dbContext.Students;
        }
        public async Task<List<Student>> GetStudentsListAsync()
        {
          return await  _students. Include(p=>p.Department).ToListAsync();
        }
    }
}
