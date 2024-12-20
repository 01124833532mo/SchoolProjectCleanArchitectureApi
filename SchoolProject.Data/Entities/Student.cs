using SchoolProject.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Student : GeneralLocalizableEntity
    {
        [Key]
        public int Id { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }


        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Phone { get; set; }
        public int? DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]

        [InverseProperty(nameof(Department.Students))]
        public virtual Department Department { get; set; }

    }
}
