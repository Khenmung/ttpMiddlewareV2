using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class EmpEmployeeGroup
    {
        [Key]
        public short EmployeeGroupId { get; set; }
        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }
        public short EmpGradeId { get; set; }
        public short DepartmentId { get; set; }
        public short? WorkAccountId { get; set; }
        public short? JobTitleId { get; set; }
        public short? DesignationId { get; set; }
        public short GroupTypeId { get; set; }
        public short? SubOrgId { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? ApprovedBy { get; set; }
        public byte Active { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmpEmployeeGroupOrgs))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(SubOrgId))]
        [InverseProperty(nameof(Organization.EmpEmployeeGroupSubOrgs))]
        public virtual Organization SubOrg { get; set; }
    }
}
