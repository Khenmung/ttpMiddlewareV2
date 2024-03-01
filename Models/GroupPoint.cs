using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("GroupPoint")]
    public partial class GroupPoint
    {
        [Key]
        public int GroupPointId { get; set; }
        public int GroupId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Points { get; set; }
        public byte? Gold { get; set; }
        public byte? Silver { get; set; }
        public byte? Bronze { get; set; }
        public int? SessionId { get; set; }
        public short? OrgId { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }
    }
}
