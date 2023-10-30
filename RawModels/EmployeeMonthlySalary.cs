using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("EmployeeMonthlySalary")]
    public class RawEmployeeMonthlySalary
    {
        [Key]
        public short EmployeeMonthlySalaryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SalaryMonth { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Deduction { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal NetSalary { get; set; }
        public short PresentDays { get; set; }
        public int EmployeeId { get; set; }
        public byte? Released { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public bool? Active { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
