using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("SyllabusDetail")]
    public partial class SyllabusDetail
    {
        public SyllabusDetail()
        {
            QuestionBanks = new HashSet<QuestionBank>();
        }

        [Key]
        public int SyllabusId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int ContentUnitId { get; set; }
        public int? SubContentUnitId { get; set; }
        [StringLength(250)]
        public string Lesson { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }

        [InverseProperty(nameof(QuestionBank.Syllabus))]
        public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
    }
}
