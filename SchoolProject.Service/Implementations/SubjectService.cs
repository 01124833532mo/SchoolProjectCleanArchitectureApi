using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.InfrastructureBases.UnitOfWork;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
        {
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Subjects>> GetSubjectsAsync()
        {
            return await _unitOfWork.SubjectRepository.GetSubjectsListAsync();
        }
        public async Task<string> AddAsync(Subjects subjects)
        {
            await GetUnitOfWork().AddAsync(subjects);
            await _unitOfWork.CompleteAsync();
            return "Success";
        }



        public async Task<string> EditAsync(Subjects subject)
        {
            await GetUnitOfWork().UpdateAsync(subject);
            await _unitOfWork.CompleteAsync();

            return "Success";
        }



        public async Task<Subjects> GetByIdAsync(int id)
        {
            var subject = await GetUnitOfWork().GetByIdAsync(id);
            return subject;
        }



        public async Task<bool> IsNameArExist(string name)
        {
            var subject = GetUnitOfWork().GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefault();

            if (subject == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var student = await GetUnitOfWork().GetTableNoTracking().Where(x => x.NameAr.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string name)
        {
            var subject = GetUnitOfWork().GetTableNoTracking().Where(x => x.NameEn.Equals(name)).FirstOrDefault();

            if (subject == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var student = await GetUnitOfWork().GetTableNoTracking().Where(x => x.NameEn.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }
        private IGenericRepository<Subjects> GetUnitOfWork()
        {
            return _unitOfWork.GetRepository<Subjects>();
        }


    }
}
