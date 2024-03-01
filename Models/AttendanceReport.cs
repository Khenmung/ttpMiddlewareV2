using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class AttendanceReport
    {
        [Key]
        public int AttendanceReportId { get; set; }
        public int EmployeeId { get; set; }
        public int MonthYear { get; set; }
        public byte Presents { get; set; }
        public byte WeekOffs { get; set; }
        public byte Holiday { get; set; }
        public byte LeaveAjusted { get; set; }
        public byte PaidDays { get; set; }
        public byte LossOfPay { get; set; }
        public short OrgId { get; set; }
        public short FinancialYear { get; set; }
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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(AttendanceReportId))]
        [InverseProperty(nameof(EmpEmployee.AttendanceReport))]
        public virtual EmpEmployee AttendanceReportNavigation { get; set; }
        [ForeignKey(nameof(FinancialYear))]
        [InverseProperty(nameof(Batch.AttendanceReports))]
        public virtual Batch FinancialYearNavigation { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.AttendanceReports))]
        public virtual Organization Org { get; set; }
    }
}
