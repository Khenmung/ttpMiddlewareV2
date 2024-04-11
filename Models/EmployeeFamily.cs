using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmployeeFamily")]
    public partial class EmployeeFamily
    {
        [Key]
        public int EmployeeFamilyId { get; set; }
        public int EmployeeId { get; set; }
        public int FamilyRelationShipId { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        public byte Age { get; set; }
        public int Gender { get; set; }
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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.EmployeeFamilies))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(FamilyRelationShipId))]
        [InverseProperty(nameof(MasterItem.EmployeeFamilyFamilyRelationShips))]
        public virtual MasterItem FamilyRelationShip { get; set; }
        [ForeignKey(nameof(Gender))]
        [InverseProperty(nameof(MasterItem.EmployeeFamilyGenderNavigations))]
        public virtual MasterItem GenderNavigation { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmployeeFamilies))]
        public virtual Organization Org { get; set; }
    }
}
