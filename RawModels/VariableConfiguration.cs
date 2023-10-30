using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("VariableConfiguration")]
    public class RawVariableConfiguration
    {
        [Key]
        public short VariableConfigurationId { get; set; }
        [Required]
        [StringLength(15)]
        public string VariableName { get; set; }
        [StringLength(100)]
        public string VariableDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VariableAmount { get; set; }
        [Required]
        [StringLength(250)]
        public string VariableFormula { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short ApplicationId { get; set; }
        public byte DisplayOrder { get; set; }
        public short? VariableTypeId { get; set; }
        public bool Deleted { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }

    }
}
