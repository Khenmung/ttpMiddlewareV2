using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("GeneralLedger")]
    public class RawGeneralLedger
    {
       
        [Key]
        public int GeneralLedgerId { get; set; }
        [Required]
        [StringLength(50)]
        public string GeneralLedgerName { get; set; }
        public int AccountNatureId { get; set; }
        public int AccountGroupId { get; set; }
        public int? AccountSubGroupId { get; set; }
        public int? StudentClassId { get; set; }
        public int? EmployeeId { get; set; }
        public short IncomeStatementSequence { get; set; }
        public short IncomeStatementPlus { get; set; }
        public short TBSequence { get; set; }
        public short TBPlus { get; set; }
        public short ExpenseSequence { get; set; }
        public short ExpensePlus { get; set; }
        public short AssetSequence { get; set; }
        public short AssetPlus { get; set; }
        public short LnESequence { get; set; }
        public short LnEPlus { get; set; }
        [StringLength(30)]
        public string ContactNo { get; set; }
        [StringLength(30)]
        public string ContactName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        public short? BatchId { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }

        
    }
}
