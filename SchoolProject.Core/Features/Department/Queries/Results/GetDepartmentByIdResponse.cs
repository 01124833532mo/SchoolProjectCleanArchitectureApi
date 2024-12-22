namespace SchoolProject.Core.Features.Department.Queries.Results
{
    public class GetDepartmentByIdResponse
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<StudentResponse>? studentList { get; set; }
        public List<SubjectResponse>? subjectList { get; set; }
        public List<InstractorsResponse>? InstractorList { get; set; }


    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class InstractorsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
