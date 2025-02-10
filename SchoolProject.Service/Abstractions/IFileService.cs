
using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstractions
{
    public interface IFileService
    {
        public Task<string> UploadImage(string location, IFormFile file);
    }
}
