using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentActivity")]
    public class RawStudentActivity
    {
        [Key]
        public short StudentActivityId { get; set; }
        public int? StudentClassId { get; set; }
        public int? StudentId { get; set; }
        public int? GroupId { get; set; }
        [StringLength(1000)]
        public string Activity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivityDate { get; set; }
        public byte? Active { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? OrgId { get; set; }
        public short? BatchId { get; set; }
        public short? TeacherId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }

      
    }
}
