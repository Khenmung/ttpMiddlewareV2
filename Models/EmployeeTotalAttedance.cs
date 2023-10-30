using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmployeeTotalAttedance")]
    public partial class EmployeeTotalAttedance
    {
        [Key]
        public int EmployeeTotalAttendanceId { get; set; }
        public int MonthYear { get; set; }
        public byte TotalNoOfAttedance { get; set; }
        public bool Active { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
    }
}
