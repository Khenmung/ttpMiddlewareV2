using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class LeaveEmployeeLeaf
    {
        [Key]
        public int EmployeeLeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LeaveFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LeaveTo { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal NoOfDays { get; set; }
        [Required]
        [StringLength(250)]
        public string LeaveReason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApplyDate { get; set; }
        public int? LeaveStatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApproveRejectedDate { get; set; }
        public int? ApprovedBy { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public short BatchId { get; set; }
        public int? YearMonth { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.LeaveEmployeeLeaves))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(LeaveStatusId))]
        [InverseProperty(nameof(MasterItem.LeaveEmployeeLeaves))]
        public virtual MasterItem LeaveStatus { get; set; }
        [ForeignKey(nameof(LeaveTypeId))]
        [InverseProperty(nameof(LeavePolicy.LeaveEmployeeLeaves))]
        public virtual LeavePolicy LeaveType { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.LeaveEmployeeLeaves))]
        public virtual Organization Org { get; set; }
    }
}
