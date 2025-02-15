using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Abstracts.Functions;

namespace SchoolProject.Infrastructure.InfrastructureBases.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<T> GetRepository<T>() where T : class;

        public Task<int> CompleteAsync();


        IStudentRepository StudentRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IInstructorFunctionsRepository InstructorFunctionsRepository { get; }

    }
}
