using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("EvaluationMaster")]
    public class RawEvaluationMaster
    {
        

        [Key]
        public int EvaluationMasterId { get; set; }
        [Required]
        [StringLength(50)]
        public string EvaluationName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public int ClassGroupId { get; set; }
        public short? Duration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        public bool? DisplayResult { get; set; }
        public bool? AppendAnswer { get; set; }
        public bool? ProvideCertificate { get; set; }
        public byte? FullMark { get; set; }
        public byte? PassMark { get; set; }
        public bool Confidential { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
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
