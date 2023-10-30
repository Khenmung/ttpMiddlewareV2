using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentClassSubject")]
    public class RawStudentClassSubject
    {
      

        [Key]
        public int StudentClassSubjectId { get; set; }
        public int ClassSubjectId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
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
