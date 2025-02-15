using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    internal class InstructorsRepository : GenericRepository<Instructor>, IInstructorsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InstructorsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
