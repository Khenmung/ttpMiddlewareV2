using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("SchoolTimeTable")]
    public class RawSchoolTimeTable
    {
        [Key]
        public int TimeTableId { get; set; }
        public int DayId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int SchoolClassPeriodId { get; set; }
        public int? TeacherSubjectId { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
