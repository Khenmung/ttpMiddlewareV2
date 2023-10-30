using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("SubjectComponent")]
    public class RawSubjectComponent
    {
        [Key]
        public int SubjectComponentId { get; set; }
        [Required]
        [StringLength(50)]
        public string ComponentName { get; set; }
        public int ClassSubjectId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public byte Sequence { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

       
    }
}
