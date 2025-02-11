using System.Data.Common;

namespace SchoolProject.Infrastructure.Abstracts.Functions
{
    public interface IInstructorFunctionsRepository
    {

        public decimal GetSalarySummationOfInstructor(string query, DbCommand cmd);
    }
}
