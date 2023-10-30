using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("AssetAssign")]
    public partial class AssetAssign
    {
        [Key]
        public int AssetAssignId { get; set; }
        public int AssetId { get; set; }
        public short? Quantity { get; set; }
        public int EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AssignFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AssignTo { get; set; }
        public int? AssignedLocationId { get; set; }
        public bool? Returned { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReturnedDate { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public short? OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        public int SubOrgId { get; set; }
    }
}
