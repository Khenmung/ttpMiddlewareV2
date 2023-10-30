using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("FeeDefinition")]
    public class RawFeeDefinition
    {
     

        [Key]
        public short FeeDefinitionId { get; set; }
        [Required]
        [StringLength(50)]
        public string FeeName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public int? FeeCategoryId { get; set; }
        public int? FeeSubCategoryId { get; set; }
        public byte? AmountEditable { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
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
