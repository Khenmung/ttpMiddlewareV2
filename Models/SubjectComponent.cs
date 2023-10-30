using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("SubjectComponent")]
    public partial class SubjectComponent
    {
        [Key]
        public int SubjectComponentId { get; set; }
        [Required]
        [StringLength(50)]
        public string ComponentName { get; set; }
        public int ClassSubjectId { get; set; }
        public int? ClassId { get; set; }
        public byte? Sequence { get; set; }
        public int? SubjectId { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        public short SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }

        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("SubjectComponents")]
        public virtual ClassSubject ClassSubject { get; set; }
    }
}
