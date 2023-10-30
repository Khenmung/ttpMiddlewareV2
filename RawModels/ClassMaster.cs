using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawClassMaster
    {
        public RawClassMaster()
        {
           
        }

        [Key]
        public int ClassId { get; set; }
        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }
        public int? DurationId { get; set; }
        public byte? MinStudent { get; set; }
        public short? MaxStudent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? StudyAreaId { get; set; }
        public int? StudyModeId { get; set; }
        public int? CategoryId { get; set; }
        public short? BatchId { get; set; }
        public byte? Sequence { get; set; }
        public bool? Confidential { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

      
    }
}
