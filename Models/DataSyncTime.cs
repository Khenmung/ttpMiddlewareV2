using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Keyless]
    [Table("DataSyncTime")]
    public partial class DataSyncTime
    {
        public int DataSyncTimeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SyncDateTime { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
    }
}
