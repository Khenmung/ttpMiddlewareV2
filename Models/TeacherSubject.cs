using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("TeacherSubject")]
    public partial class TeacherSubject
    {
        public TeacherSubject()
        {
            SchoolTimeTables = new HashSet<SchoolTimeTable>();
        }

        [Key]
        public int TeacherSubjectId { get; set; }
        public int ClassSubjectId { get; set; }
        public int EmployeeId { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("TeacherSubjects")]
        public virtual ClassSubject ClassSubject { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.TeacherSubjects))]
        public virtual EmpEmployee Employee { get; set; }
        [InverseProperty(nameof(SchoolTimeTable.TeacherSubject))]
        public virtual ICollection<SchoolTimeTable> SchoolTimeTables { get; set; }
    }
}
