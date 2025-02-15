using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Abstracts.Functions;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Repositories.Functions;
using System.Collections.Concurrent;

namespace SchoolProject.Infrastructure.InfrastructureBases.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _repositories;
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IStudentRepository> _studentRepository;
        private readonly Lazy<ISubjectRepository> _subjectRepository;
        private readonly Lazy<IInstructorFunctionsRepository> _instructorFunctionsRepository;


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _repositories = new ConcurrentDictionary<string, object>();
            _dbContext = dbContext;
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(_dbContext));
            _subjectRepository = new Lazy<ISubjectRepository>(() => new SubjectRepository(_dbContext));
            _instructorFunctionsRepository = new Lazy<IInstructorFunctionsRepository>(() => new InstructorFunctionsRepository());
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return (IGenericRepository<T>)_repositories.GetOrAdd(typeof(T).Name, new GenericRepository<T>(_dbContext));
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }


        public IStudentRepository StudentRepository => _studentRepository.Value;

        public ISubjectRepository SubjectRepository => _subjectRepository.Value;

        public IInstructorFunctionsRepository InstructorFunctionsRepository => _instructorFunctionsRepository.Value;
    }
}
