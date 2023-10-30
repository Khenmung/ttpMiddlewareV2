using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ClassSubject")]
    public class RawClassSubject
    {
        public RawClassSubject()
        {

        }

        [Key]
        public int ClassSubjectId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SemesterId { get; set; }
        public int SubjectId { get; set; }
        public short SubjectTypeId { get; set; }
        public int? SubjectCategoryId { get; set; }
        public byte? Credits { get; set; }
        public bool? Confidential { get; set; }
        public int? TeacherId { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short? BatchId { get; set; }
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

    }
      
    
}
