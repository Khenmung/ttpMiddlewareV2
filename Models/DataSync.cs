using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("DataSync")]
    public partial class DataSync
    {
        [Key]
        public int DataSyncId { get; set; }
        [Required]
        [StringLength(50)]
        public string TableName { get; set; }
        [Required]
        [StringLength(2000)]
        public string Text { get; set; }
        [Required]
        [StringLength(10)]
        public string DataMode { get; set; }
        public bool Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public short BatchId { get; set; }
        public bool Deleted { get; set; }
        public bool Synced { get; set; }
        public Guid SyncId { get; set; }
        public bool? History { get; set; }
    }
}
