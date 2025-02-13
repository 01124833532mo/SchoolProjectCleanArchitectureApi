using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<List<Subjects>> GetSubjectsAsync()
        {
            return await _subjectRepository.GetSubjectsListAsync();
        }
        public async Task<string> AddAsync(Subjects subjects)
        {
            await _subjectRepository.AddAsync(subjects);
            return "Success";
        }

        public async Task<string> EditAsync(Subjects subject)
        {
            await _subjectRepository.UpdateAsync(subject);
            return "Success";
        }



        public async Task<Subjects> GetByIdAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            return subject;
        }



        public async Task<bool> IsNameArExist(string name)
        {
            var subject = _subjectRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefault();

            if (subject == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var student = await _subjectRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string name)
        {
            var subject = _subjectRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name)).FirstOrDefault();

            if (subject == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var student = await _subjectRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }


    }
}
