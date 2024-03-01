using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentStature")]
    public partial class StudentStature
    {
        [Key]
        public int StudentStatureId { get; set; }
        public int StudentClassId { get; set; }
        public short ClassId { get; set; }
        public int StatusId { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public short BatchId { get; set; }
        public short ExamId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentStatures")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
