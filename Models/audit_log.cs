using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Keyless]
    [Table("audit_log")]
    public partial class audit_log
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string PackageName { get; set; }
        [StringLength(200)]
        public string TableName { get; set; }
        public int? RecordsInserted { get; set; }
        public int? RecordsUpdated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
