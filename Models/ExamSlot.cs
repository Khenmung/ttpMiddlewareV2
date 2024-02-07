using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ExamSlot")]
    public partial class ExamSlot
    {
        public ExamSlot()
        {
            SlotAndClassSubjects = new HashSet<SlotAndClassSubject>();
        }

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
        public short ClassGroupId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("ExamSlots")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ExamSlots")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ExamSlots))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(SlotAndClassSubject.Slot))]
        public virtual ICollection<SlotAndClassSubject> SlotAndClassSubjects { get; set; }
    }
}
