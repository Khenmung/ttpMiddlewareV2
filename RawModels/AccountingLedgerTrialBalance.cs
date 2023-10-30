using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("AccountingLedgerTrialBalance")]
    public class RawAccountingLedgerTrialBalance
    {
        public RawAccountingLedgerTrialBalance()
        {
            
        }

        [Key]
        public int LedgerId { get; set; }
        public int? StudentClassId { get; set; }
        public int? EmployeeId { get; set; }
        public int? GeneralLedgerId { get; set; }
        public int? Month { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BaseAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalDebit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCredit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

     
       
    }
}
