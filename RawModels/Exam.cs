using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawExam
    {
        

        [Key]
        public short ExamId { get; set; }
        public int ExamNameId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public byte? ReleaseResult { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReleaseDate { get; set; }
        [StringLength(100)]
        public string MarkFormula { get; set; }
        public int? CategoryId { get; set; }
        public int? ClassGroupId { get; set; }
        public short ClassGroupMappingId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendanceStartDate { get; set; }
        public short? BatchId { get; set; }
        public byte? Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
