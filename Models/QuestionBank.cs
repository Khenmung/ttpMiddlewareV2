using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("QuestionBank")]
    public partial class QuestionBank
    {
        public QuestionBank()
        {
            QuestionBankNExams = new HashSet<QuestionBankNExam>();
            StorageFnPs = new HashSet<StorageFnP>();
        }

        [Key]
        public int QuestionBankId { get; set; }
        public int SyllabusId { get; set; }
        [Required]
        [StringLength(800)]
        public string Question { get; set; }
        [StringLength(100)]
        public string Diagram { get; set; }
        public int DifficultyLevelId { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(SyllabusId))]
        [InverseProperty(nameof(SyllabusDetail.QuestionBanks))]
        public virtual SyllabusDetail Syllabus { get; set; }
        [InverseProperty(nameof(QuestionBankNExam.QuestionBank))]
        public virtual ICollection<QuestionBankNExam> QuestionBankNExams { get; set; }
        [InverseProperty(nameof(StorageFnP.Question))]
        public virtual ICollection<StorageFnP> StorageFnPs { get; set; }
    }
}
