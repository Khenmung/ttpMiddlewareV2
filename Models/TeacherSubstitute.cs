using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("TeacherSubstitute")]
    public partial class TeacherSubstitute
    {
        [Key]
        public int TeacherStituteId { get; set; }
        public int ClassId { get; set; }
        public int PeriodId { get; set; }
        public int TeacherSubjectId { get; set; }
        public int SubstituteTeacherId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SubstituteDate { get; set; }
        public bool Active { get; set; }
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
        public short BatchId { get; set; }
        public int SubOrgId { get; set; }

        
        public virtual ClassMaster Class { get; set; }
    }
}
