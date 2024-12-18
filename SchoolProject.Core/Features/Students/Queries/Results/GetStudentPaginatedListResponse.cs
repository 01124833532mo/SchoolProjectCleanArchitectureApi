namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class GetStudentPaginatedListResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? DepartmentName { get; set; }


        public GetStudentPaginatedListResponse(int studid, string? name, string? address, string? departmentname)
        {

            Id = studid;
            Name = name;
            Address = address;
            DepartmentName = departmentname;

        }
    }
}
