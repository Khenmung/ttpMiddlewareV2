using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("CourseYearSemester")]
    public partial class CourseYearSemester
    {
        [Key]
        public int CourseYearSemesterId { get; set; }
        public int ClassId { get; set; }
        public int Year { get; set; }
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

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.CourseYearSemesters))]
        public virtual ClassMaster Class { get; set; }
    }
}
