using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmpManagerGroupMapping")]
    public partial class EmpManagerGroupMapping
    {
        [Key]
        public short ManagerGroupMappingId { get; set; }
        public short EmployeeGroupId { get; set; }
        public short ManagerEmployeeId { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Active { get; set; }
        public int SubOrgId { get; set; }
    }
}
