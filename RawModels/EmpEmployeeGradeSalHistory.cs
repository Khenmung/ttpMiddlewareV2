using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("EmpEmployeeGradeSalHistory")]
    public class RawEmpEmployeeGradeSalHistory
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

       
    }
}
