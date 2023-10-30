using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawClassSubjectMarkComponent
    {
        public RawClassSubjectMarkComponent()
        {
            
        }

        [Key]
        public short ClassSubjectMarkComponentId { get; set; }
        public int ClassSubjectId { get; set; }
        public int SubjectComponentId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal FullMark { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal PassMark { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? OverallPassMark { get; set; }
        public short? ExamId { get; set; }
        public byte Active { get; set; }
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
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        
       
    }
}
