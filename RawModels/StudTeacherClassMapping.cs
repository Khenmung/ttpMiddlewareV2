using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudTeacherClassMapping")]
    public class RawStudTeacherClassMapping
    {
        [Key]
        public int TeacherClassMappingId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int? HelperId { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

      
    }
}
