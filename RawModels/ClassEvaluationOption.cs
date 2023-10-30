using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawClassEvaluationOption
    {
        public RawClassEvaluationOption()
        {
           
        }

        [Key]
        public int ClassEvaluationAnswerOptionsId { get; set; }
        public int? ClassEvaluationId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public byte? Point { get; set; }
        public byte? Correct { get; set; }
        public byte Active { get; set; }
        public int? ParentId { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        
    }
}
