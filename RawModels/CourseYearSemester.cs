using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("CourseYearSemester")]
    public class RawCourseYearSemester
    {
        [Key]
        public int CourseYearSemesterId { get; set; }
        public int ClassId { get; set; }
        public int ClassYearId { get; set; }
        public int SemesterId { get; set; }
        public int? ParentId { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        public byte BatchId { get; set; }
        public byte Active { get; set; }
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
