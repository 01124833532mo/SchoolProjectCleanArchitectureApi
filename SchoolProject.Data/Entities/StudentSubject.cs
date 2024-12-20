using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public decimal? grade { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentSubjects")]
        public virtual Student? Student { get; set; }

        [ForeignKey(nameof(SubjectId))]
        [InverseProperty("StudentsSubjects")]

        public virtual Subjects? Subject { get; set; }

    }
}
