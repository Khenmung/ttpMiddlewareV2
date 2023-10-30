using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentGrade")]
    public class RawStudentGrade
    {
        [Key]
        public short StudentGradeId { get; set; }
        public short? ExamId { get; set; }
        [Required]
        [StringLength(50)]
        public string GradeName { get; set; }
        public byte? Points { get; set; }
        [Required]
        [StringLength(256)]
        public string Formula { get; set; }
        public int SubjectCategoryId { get; set; }
        public int? ClassGroupId { get; set; }
        public byte Sequence { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? GradeStatusId { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
