using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentClass")]
    [Index(nameof(ClassId), Name = "idx_ClassId")]
    public class RawStudentClass
    {
        

        [Key]
        public int StudentClassId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        [StringLength(30)]
        public string RollNo { get; set; }
        public int? HouseId { get; set; }
        public int? SectionId { get; set; }
        public short BatchId { get; set; }
        public short? FeeTypeId { get; set; }
        public int? SemesterId { get; set; }
        public bool? IsCurrent { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(10)]
        public string AdmissionNo { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public byte? Promoted { get; set; }
        [StringLength(50)]
        public string PhotoPath { get; set; }
        public bool Deleted { get; set; }

       
    }
}
