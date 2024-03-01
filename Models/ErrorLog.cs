using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        [Key]
        public int ErrorId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Detail { get; set; }
        [StringLength(50)]
        public string ModuleName { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }
        public int StatusId { get; set; }
        public int? Category { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }
    }
}
