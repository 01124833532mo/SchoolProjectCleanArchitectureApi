using SchoolProject.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Data.Entities
{
    public class Department : GeneralLocalizableEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string NameAr { get; set; }
        [StringLength(200)]

        public string NameEn { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new HashSet<DepartmentSubject>();

    }
}
