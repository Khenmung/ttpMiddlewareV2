using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("SlotAndClassSubject")]
    public partial class SlotAndClassSubject
    {
        [Key]
        public int SlotClassSubjectId { get; set; }
        public short SlotId { get; set; }
        public int ClassSubjectId { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }
        public int ClassId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("SlotAndClassSubjects")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("SlotAndClassSubjects")]
        public virtual ClassSubject ClassSubject { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.SlotAndClassSubjects))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(SlotId))]
        [InverseProperty(nameof(ExamSlot.SlotAndClassSubjects))]
        public virtual ExamSlot Slot { get; set; }
    }
}
