using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("LeavePolicy")]
    public partial class LeavePolicy
    {
        public LeavePolicy()
        {
            LeaveBalances = new HashSet<LeaveBalance>();
            LeaveEmployeeLeaves = new HashSet<LeaveEmployeeLeaf>();
        }

        [Key]
        public int LeavePolicyId { get; set; }
        public int LeaveNameId { get; set; }
        [Required]
        [StringLength(256)]
        public string FormulaOrDays { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        [StringLength(256)]
        public string ExcludeDays { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("LeavePolicies")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(LeaveNameId))]
        [InverseProperty(nameof(MasterItem.LeavePolicies))]
        public virtual MasterItem LeaveName { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.LeavePolicies))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(LeaveBalance.LeavePolicy))]
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
        [InverseProperty(nameof(LeaveEmployeeLeaf.LeaveType))]
        public virtual ICollection<LeaveEmployeeLeaf> LeaveEmployeeLeaves { get; set; }
    }
}
