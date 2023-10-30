using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttpMiddleware.Models.DTOs.Requests
{
    public class FeePaymentDTO
    {      
        public StudentFeeReceipt studentFeeReceipt { get; set; }
        public ICollection<AccountingLedgerTrialBalance> ledgerAccount { get; set; }
        public ICollection<AccountingVoucher> accountingVoucher { get; set; }
    }
}
