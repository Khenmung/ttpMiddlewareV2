using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class EmpEmployeeSalaryComponent
    {
        [Key]
        public int EmployeeSalaryComponentId { get; set; }
        public int EmployeeId { get; set; }
        public int EmpComponentId { get; set; }
        [Required]
        [StringLength(250)]
        public string ActualFormulaOrAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public short OrgId { get; set; }
        public int Month { get; set; }
        public short? BatchId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RealeasedDate { get; set; }
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
        public int DepartmentId { get; set; }
        [StringLength(256)]
        public string Description { get; set; }

        [ForeignKey(nameof(EmpComponentId))]
        [InverseProperty("EmpEmployeeSalaryComponents")]
        public virtual EmpComponent EmpComponent { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.EmpEmployeeSalaryComponents))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmpEmployeeSalaryComponents))]
        public virtual Organization Org { get; set; }
    }
}
