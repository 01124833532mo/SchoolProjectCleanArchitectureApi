using SchoolProject.Infrastructure.Abstracts.Functions;
using System.Data.Common;

namespace SchoolProject.Infrastructure.Repositories.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {
        public decimal GetSalarySummationOfInstructor(string query, DbCommand cmd)
        {
            decimal response = 0;

            cmd.CommandText = query;
            var value = cmd.ExecuteScalar();
            var result = value.ToString();

            if (decimal.TryParse(result, out decimal d))
            {
                response = d;
            }

            return response;

        }
    }
}
