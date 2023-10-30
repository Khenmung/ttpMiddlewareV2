using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("DynamicTable")]
    public partial class DynamicTable
    {
        [Key]
        [StringLength(10)]
        public string DynamicTableId { get; set; }
        [Required]
        [StringLength(50)]
        public string ColumnName { get; set; }
        public int ColumnTypeId { get; set; }
        public short ColumnSize { get; set; }
        public short OrgId { get; set; }
        public int ParentId { get; set; }
        public byte Active { get; set; }
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
