using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("TeacherPeriod")]
    public class RawTeacherPeriod
    {
        [Key]
        public int TeacherPeriodId { get; set; }
        public int EmployeeId { get; set; }
        public int SchoolClassPeriodId { get; set; }
        public int? TeacherSubjectId { get; set; }
        public bool? OffPeriod { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
