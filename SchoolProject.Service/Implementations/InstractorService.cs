using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Abstracts.Functions;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases.UnitOfWork;
using SchoolProject.Service.Abstractions;
using System.Data;

namespace SchoolProject.Service.Implementations
{
    public class InstractorService : IInstractorService
    {
        private readonly IInstructorsRepository _instructorsRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        private readonly IUnitOfWork unitOfWork;

        public InstractorService(
            IInstructorsRepository instructorsRepository,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            IInstructorFunctionsRepository instructorFunctionsRepository,
            IUnitOfWork unitOfWork)
        {
            _instructorsRepository = instructorsRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _instructorFunctionsRepository = instructorFunctionsRepository;
            this.unitOfWork = unitOfWork;
        }



        public async Task<bool> IsNameArExist(string name)
        {
            //var student = _instructorsRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefault();
            var student = unitOfWork.GetRepository<Instructor>().GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefault();

            if (student == null) return false;
            return true;

        }


        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            //var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(name) & x.InstructorId != id).FirstOrDefaultAsync();
            var student = await unitOfWork.GetRepository<Instructor>().GetTableNoTracking().Where(x => x.NameAr.Equals(name) & x.InstructorId != id).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //Check if the name is Exist Or not
            //var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn)).FirstOrDefaultAsync();
            var student = await unitOfWork.GetRepository<Instructor>().GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            //Check if the name is Exist Or not
            //var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn) & x.InstructorId != id).FirstOrDefaultAsync();
            var student = await unitOfWork.GetRepository<Instructor>().GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn) & x.InstructorId != id).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<string> AddInstractorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            instructor.Image = baseUrl + imageUrl;
            try
            {
                //await _instructorsRepository.AddAsync(instructor);
                await unitOfWork.GetRepository<Instructor>().AddAsync(instructor);
                var complete = await unitOfWork.CompleteAsync() > 0;
                if (complete)
                    return "Success";
                else
                    return "FailedInAdd";


            }
            catch (Exception)
            {
                return "FailedInAdd";
            }


        }

        public Task<decimal> GetSalarySummationOfInstructor()
        {

            decimal result = 0;

            using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //result = _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()", cmd);
                result = unitOfWork.InstructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()", cmd);
            }

            return Task.FromResult(result);


        }

        public async Task<bool> IsInstractorIdExist(int? instractorId)
        {
            return await unitOfWork.GetRepository<Instructor>().GetTableNoTracking().AnyAsync(e => e.InstructorId.Equals(instractorId));
        }
    }
}
