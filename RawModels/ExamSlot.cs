using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ExamSlot")]
    public class RawExamSlot
    {
       

        [Key]
        public short ExamSlotId { get; set; }
        public short ExamId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExamDate { get; set; }
        public short SlotNameId { get; set; }
        [Required]
        [StringLength(10)]
        public string StartTime { get; set; }
        [Required]
        [StringLength(10)]
        public string EndTime { get; set; }
        public byte? Sequence { get; set; }
        public bool Deleted { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
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
