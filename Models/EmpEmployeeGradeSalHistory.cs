using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmpEmployeeGradeSalHistory")]
    public partial class EmpEmployeeGradeSalHistory
    {
        [Key]
        public short EmployeeGradeHistoryId { get; set; }
        public int EmpGradeId { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CTC { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToDate { get; set; }
        public int? ManagerId { get; set; }
        public int? ReportingTo { get; set; }
        public short? JobTitleId { get; set; }
        public int? DesignationId { get; set; }
        public int? WorkAccountId { get; set; }
        public byte? IsCurrent { get; set; }
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
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty(nameof(MasterItem.EmpEmployeeGradeSalHistoryDepartments))]
        public virtual MasterItem Department { get; set; }
        [ForeignKey(nameof(DesignationId))]
        [InverseProperty(nameof(MasterItem.EmpEmployeeGradeSalHistoryDesignations))]
        public virtual MasterItem Designation { get; set; }
        [ForeignKey(nameof(EmpGradeId))]
        [InverseProperty(nameof(MasterItem.EmpEmployeeGradeSalHistoryEmpGrades))]
        public virtual MasterItem EmpGrade { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.EmpEmployeeGradeSalHistoryEmployees))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(ManagerId))]
        [InverseProperty(nameof(EmpEmployee.EmpEmployeeGradeSalHistoryManagers))]
        public virtual EmpEmployee Manager { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmpEmployeeGradeSalHistories))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(WorkAccountId))]
        [InverseProperty(nameof(MasterItem.EmpEmployeeGradeSalHistoryWorkAccounts))]
        public virtual MasterItem WorkAccount { get; set; }
    }
}
