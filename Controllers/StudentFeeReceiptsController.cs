using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using ttpMiddleware.CommonFunctions;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Data.Entities;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery(MaxExpansionDepth = 3)]
    public class StudentFeeReceiptsController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentFeeReceiptsController(ttpauthContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/StudentFeeReceipts
        [HttpGet]
        public IQueryable<StudentFeeReceipt> GetStudentFeeReceipts()
        {
            return _context.StudentFeeReceipts.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentFeeReceipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentFeeReceipt>> GetStudentFeeReceipt(int id)
        {
            var studentFeeReceipt = await _context.StudentFeeReceipts.FindAsync(id);

            if (studentFeeReceipt == null)
            {
                return NotFound();
            }

            return studentFeeReceipt;
        }

        // PUT: api/StudentFeeReceipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentFeeReceipt(int id, StudentFeeReceipt studentFeeReceipt)
        {
            if (id != studentFeeReceipt.StudentFeeReceiptId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(studentFeeReceipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeeReceiptExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentFeeReceipt> studentFeeReceipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var feeReceipt = await _context.StudentFeeReceipts.FindAsync(key);
            if (feeReceipt == null)
            {
                return NotFound();
            }
            using var tran = _context.Database.BeginTransaction();
            studentFeeReceipt.Patch(feeReceipt);
            try
            {
                //var _studentLedgerId = await _context.GeneralLedgers.Where(x => x.StudentClassId == feeReceipt.StudentClassId).Select(s => s.GeneralLedgerId).FirstOrDefaultAsync();
                //zero means cancelled
                if (feeReceipt.Active == 0)
                {
                    //var paymentType = await _context.MasterItems.Where(x => x.MasterDataId == feeReceipt.PaymentTypeId
                    //&& x.OrgId == feeReceipt.OrgId
                    //&& x.SubOrgId == feeReceipt.SubOrgId
                    //).Select(s => s.MasterDataName).FirstOrDefaultAsync();
                    ////revert cash/bank account by crediting to cash account.
                    //Int32? _generalAccountId = 0;
                    //bool _debit = false;
                    //if (paymentType.ToLower() == "ats")
                    //{
                    //    _generalAccountId = feeReceipt.AdjustedAccountId;
                    //    _debit = true;
                    //}
                    //else
                    //{
                    //    if (paymentType.ToLower() == "cash")
                    //        paymentType = "cash account";
                    //    else if (paymentType.ToLower() == "online")
                    //        paymentType = "bank account";
                    //    _debit = false;
                    //    var _cashOrBankAccountOrOther = await _context.GeneralLedgers.Where(x => x.GeneralLedgerName.ToLower() == paymentType.ToLower()
                    //    && x.OrgId == feeReceipt.OrgId
                    //    && x.SubOrgId == feeReceipt.SubOrgId
                    //    ).FirstOrDefaultAsync();
                    //    _generalAccountId = _cashOrBankAccountOrOther.GeneralLedgerId;
                    //}
                    //if (_generalAccountId > 0)
                    //{
                    //    var _cashAccountToAddToAccountingVoucher = new AccountingVoucher()
                    //    {
                    //        DocDate = DateTime.Now,
                    //        PostingDate = DateTime.Now,
                    //        Amount = feeReceipt.TotalAmount,
                    //        GeneralLedgerAccountId = _generalAccountId,
                    //        Debit = _debit,
                    //        Active = 1,
                    //        Deleted = false,
                    //        FeeReceiptId = feeReceipt.StudentFeeReceiptId,
                    //        Reference = feeReceipt.ReceiptNo.ToString(),//).Replace(" ", "").Substring(0, 10) + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    //        ShortText = "Cancelled",
                    //        OrgId = feeReceipt.OrgId,
                    //        SubOrgId = feeReceipt.SubOrgId,
                    //        LedgerId = 0,
                    //        Balance = 0,
                    //        BaseAmount = feeReceipt.TotalAmount,
                    //    };
                    //    _context.AccountingVouchers.Add(_cashAccountToAddToAccountingVoucher);
                    //}
                    ////revert student account
                    //revert tuition fee account
                    //var _tuitionFeeAccount = await _context.GeneralLedgers.Where(x => x.GeneralLedgerName.ToLower() == "tuition fee"
                    //&& x.OrgId == feeReceipt.OrgId
                    //&& x.SubOrgId == feeReceipt.SubOrgId
                    //).FirstOrDefaultAsync();
                    //if (_tuitionFeeAccount != null)
                    //{
                    //    var _studentAccountToAddToAccountingVoucher = new AccountingVoucher()
                    //    {
                    //        DocDate = DateTime.Now,
                    //        PostingDate = DateTime.Now,
                    //        Amount = feeReceipt.TotalAmount,
                    //        GeneralLedgerAccountId = _tuitionFeeAccount.GeneralLedgerId,
                    //        Debit = true,
                    //        Active = 1,
                    //        Deleted = false,
                    //        FeeReceiptId = feeReceipt.StudentFeeReceiptId,
                    //        ShortText = "Cancelled Receipt no. " + feeReceipt.ReceiptNo,
                    //        Reference = feeReceipt.ReceiptNo.ToString(),
                    //        OrgId = feeReceipt.OrgId,
                    //        SubOrgId = feeReceipt.SubOrgId,
                    //        LedgerId = 0,
                    //        Balance = 0,
                    //        BaseAmount = feeReceipt.TotalAmount,
                    //    };
                    //    _context.AccountingVouchers.Add(_studentAccountToAddToAccountingVoucher);
                    //}
                    //else
                    //{
                    //    return BadRequest("tuition fee account not defined.");

                    //}
                    ////revert accounts ends here

                    ////revert  discount allowed account
                    //var _discountAllowedAccount = await _context.GeneralLedgers
                    //    .Join(_context.AccountingVouchers,
                    //    general => general.GeneralLedgerId,
                    //    accv => accv.GeneralLedgerAccountId, (general, accv) => new { accv.FeeReceiptId, general.GeneralLedgerId, general.GeneralLedgerName, general.OrgId, accv.BaseAmount })
                    //    .Where(x => x.GeneralLedgerName.ToLower() == "discount allowed"
                    //    && x.FeeReceiptId == feeReceipt.StudentFeeReceiptId
                    //    && x.OrgId == feeReceipt.OrgId).FirstOrDefaultAsync();
                    //if (_discountAllowedAccount != null)
                    //{
                    //    var _discountAllowedToAddToAccountingVoucher = new AccountingVoucher()
                    //    {
                    //        DocDate = DateTime.Now,
                    //        PostingDate = DateTime.Now,
                    //        Amount = (decimal)_discountAllowedAccount.BaseAmount,
                    //        GeneralLedgerAccountId = _discountAllowedAccount.GeneralLedgerId,
                    //        Debit = false,
                    //        Active = 1,
                    //        Deleted = false,
                    //        FeeReceiptId = feeReceipt.StudentFeeReceiptId,
                    //        ShortText = "Receipt no. " + feeReceipt.ReceiptNo,
                    //        OrgId = feeReceipt.OrgId,
                    //        LedgerId = 0,
                    //        Balance = 0,
                    //        BaseAmount = (decimal)_discountAllowedAccount.BaseAmount
                    //    };
                    //    _context.AccountingVouchers.Add(_discountAllowedToAddToAccountingVoucher);
                    //}
                    ////revert discount allowed ends here

                    //
                    var accountVouchers = await _context.AccountingVouchers
                        .Where(s => s.FeeReceiptId == feeReceipt.StudentFeeReceiptId
                        && s.OrgId == feeReceipt.OrgId
                        && s.SubOrgId == feeReceipt.SubOrgId
                        ).ToListAsync();
                    foreach (var av in accountVouchers)
                    {
                        //deactivating the item
                        av.Active = 0;

                        //updating balance and paid amount of the reference
                        var _reference = await _context.AccountingVouchers
                            .Where(x => x.ClassFeeId == av.ClassFeeId
                            && x.LedgerId == av.LedgerId
                            && x.FeeReceiptId == 0
                            && x.OrgId == av.OrgId
                            && x.SubOrgId == av.SubOrgId
                            ).FirstOrDefaultAsync();
                        if (_reference != null)
                        {
                            _reference.Amount = _reference.Amount - av.Amount;
                            _reference.Balance = (decimal)(av.Balance + av.Amount);
                            //_reference.MainItem = false;
                            _context.AccountingVouchers.Update(_reference);
                        }
                    }
                    var _distinctLedgerId = accountVouchers.GroupBy(g => g.LedgerId, (key, c) => c.FirstOrDefault()).Select(s => s.LedgerId);

                    foreach (var lId in _distinctLedgerId)
                    {

                        var gl = await _context.AccountingLedgerTrialBalances
                            .Where(w => w.LedgerId == lId).ToListAsync();
                        var _cancelAmountOfALedger = accountVouchers.Where(x => x.LedgerId == lId && x.FeeReceiptId > 0 && x.ClassFeeId > 0).Sum(s => s.Amount);

                        foreach (var item in gl)
                        {

                            item.TotalCredit = (decimal)(item.TotalCredit - _cancelAmountOfALedger);
                            item.Balance = item.TotalDebit - item.TotalCredit;

                            //after updating TotalCredit, if totalcredit is zero, ther is no more active bill for this receipt no.
                            if (item.TotalCredit == 0)
                            {
                                item.Active = 0;
                                _context.AccountingLedgerTrialBalances.Update(item);
                                var acledger = new AccountingLedgerTrialBalance()
                                {
                                    LedgerId = 0,
                                    OrgId = item.OrgId,
                                    SubOrgId = item.SubOrgId,
                                    Active = 1,
                                    Month = item.Month,
                                    StudentClassId = item.StudentClassId,
                                    TotalCredit = 0,
                                    //GeneralLedgerId = _studentLedgerId,
                                    TotalDebit = item.TotalDebit,
                                    Balance = item.TotalDebit,
                                    BaseAmount = item.BaseAmount,
                                    CreatedDate = DateTime.Now,
                                    BatchId = item.BatchId
                                };
                                _context.AccountingLedgerTrialBalances.Add(acledger);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();

                }

                tran.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                tran.Rollback();
                if (!StudentFeeReceiptExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(feeReceipt);
        }
        // POST: api/StudentFeeReceipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentFeeReceipt>> PostStudentFeeReceipt([FromBody] JObject jsonWrapper)
        {
            JToken jsonValues = jsonWrapper;
            StudentFeeReceipt _StudentFeeReceipt = new StudentFeeReceipt();
            List<AccountingLedgerTrialBalance> _AccountingLedgerTrialBalance = new List<AccountingLedgerTrialBalance>();
            List<AccountingVoucher> _AccountingVoucher = new List<AccountingVoucher>();
            using var tran = _context.Database.BeginTransaction();
            try
            {
                foreach (JProperty x in jsonValues)
                {
                    if (x.Name == "StudentFeeReceipt")
                        _StudentFeeReceipt = x.Value.ToObject<StudentFeeReceipt>();
                    else if (x.Name == "LedgerAccount")
                    {
                        _AccountingLedgerTrialBalance = x.Value.ToObject<List<AccountingLedgerTrialBalance>>();

                        //foreach (var y in (JToken)x.Value)
                        //    _AccountingLedgerTrialBalance.Add(y.ToObject<AccountingLedgerTrialBalance>());

                    }
                    else if (x.Name == "AccountingVoucher")
                        _AccountingVoucher = x.Value.ToObject<List<AccountingVoucher>>();

                }


                

                var receiptNo = await _context.StudentFeeReceipts.Where(s =>
                s.OrgId == _StudentFeeReceipt.OrgId
                && s.SubOrgId == _StudentFeeReceipt.SubOrgId)
                    .DefaultIfEmpty().MaxAsync(p => p == null ? 0 : p.ReceiptNo);

                _StudentFeeReceipt.ReceiptNo = receiptNo + 1;

                _context.StudentFeeReceipts.Add(_StudentFeeReceipt);

                //student class becomes active after fee payment.
                var studcls = await _context.StudentClasses.Where(x => x.StudentClassId == _StudentFeeReceipt.StudentClassId && x.Active == 0).FirstOrDefaultAsync();
                if (studcls != null)
                {
                    studcls.Active = 1;
                    studcls.AdmissionDate = DateTime.Now;
                    _context.Update(studcls);
                }
                //ends student class becomes active after fee payment

                var _debit = false;
                var _generalLedgerAccountId = 0;
                var _ledgerName = "";
                var _ledgerAccountName = await _context.MasterItems.Where(x => x.MasterDataId == _StudentFeeReceipt.PaymentTypeId
                                        && x.OrgId == _StudentFeeReceipt.OrgId
                                        && x.SubOrgId == _StudentFeeReceipt.SubOrgId).Select(s => s.MasterDataName).FirstOrDefaultAsync();
                if (_ledgerAccountName.ToLower() == "ats")
                {
                    _debit = true;
                    _generalLedgerAccountId = (int)_StudentFeeReceipt.AdjustedAccountId;
                    //_ledgerName = "salary account";
                }
                else
                {
                    if (_ledgerAccountName.ToLower() == "cash")
                    {
                        _ledgerName = "cash account";
                    }
                    else if (_ledgerAccountName.ToLower() == "online")
                    {
                        _ledgerName = "bank account";
                    }

                    var _generalAccount = await _context.GeneralLedgers.Where(x => x.GeneralLedgerName.ToLower() == _ledgerName
                    && x.OrgId == _StudentFeeReceipt.OrgId
                    && x.SubOrgId == _StudentFeeReceipt.SubOrgId).FirstOrDefaultAsync();
                    _debit = true;
                    _generalLedgerAccountId = (int)_generalAccount.GeneralLedgerId;
                }
                //var _studentLedgerId = await _context.GeneralLedgers.Where(x => x.StudentClassId == _StudentFeeReceipt.StudentClassId)
                //                            .Select(s => s.GeneralLedgerId).FirstOrDefaultAsync();
                //var _accountReceivableId = await _context.GeneralLedgers.Where(x =>
                //x.OrgId == _StudentFeeReceipt.OrgId
                //&& x.SubOrgId == _StudentFeeReceipt.SubOrgId
                //&& (x.GeneralLedgerName.ToLower() == "discount allowed" || x.GeneralLedgerName.ToLower() == "account receivable"))
                //                            .Select(s => new { s.GeneralLedgerId, GeneralLedgerName = s.GeneralLedgerName.ToLower() }).ToListAsync();



                var reference = "";
                foreach (var item in _AccountingLedgerTrialBalance)
                {
                    if (item.LedgerId == 0)
                    {
                        //item.GeneralLedgerId = _studentLedgerId;
                        _context.AccountingLedgerTrialBalances.Add(item);
                    }
                    else
                    {
                        var entity = await _context.AccountingLedgerTrialBalances.FindAsync(item.LedgerId);
                        if (entity != null)
                        {
                            entity.TotalCredit += item.TotalCredit;
                            entity.Balance = item.Balance;
                            entity.ClassId = item.ClassId;
                            entity.SectionId = item.SectionId;
                            entity.SemesterId= item.SemesterId;
                            //entity.GeneralLedgerId = _studentLedgerId;
                            _context.Update<AccountingLedgerTrialBalance>(entity);
                        }
                    }
                    await _context.SaveChangesAsync();

                    var Details = _AccountingVoucher.Where(x => x.Month == item.Month || x.Month == 0);
                    foreach (var detail in Details)
                    {
                        if (detail.AccountingVoucherId > 0)
                        {
                            var _paidbefore = await _context.AccountingVouchers
                                .Where(x => x.AccountingVoucherId == detail.AccountingVoucherId
                                && x.OrgId == detail.OrgId
                                && x.SubOrgId == detail.SubOrgId).FirstOrDefaultAsync();
                            _paidbefore.Balance = detail.Balance;
                            _paidbefore.Amount = _paidbefore.Amount + detail.Amount;
                            _context.AccountingVouchers.Update(_paidbefore);

                            /////updated for accounting purpose
                            //var objIndex = _accountReceivableId.Select(o => o.GeneralLedgerName).ToList().IndexOf("account receivable");
                            //var accountReceivableId = _accountReceivableId[objIndex].GeneralLedgerId;

                            ////to balance out with previous "account receivable" just making Debit to false;
                            //detail.FeeReceiptId = 0;
                            //detail.GeneralLedgerAccountId = accountReceivableId;
                            //detail.AccountingVoucherId = 0;
                            //detail.Debit = false;
                            //detail.Amount = _paidbefore.Balance;
                            //_context.AccountingVouchers.Add(detail);
                            //// if there is still balance.
                            //if (detail.Balance > 0)
                            //{
                            //    detail.FeeReceiptId = 0;
                            //    detail.GeneralLedgerAccountId = accountReceivableId;
                            //    detail.AccountingVoucherId = 0;
                            //    detail.Debit = true;
                            //    detail.Amount = detail.Balance;
                            //    _context.AccountingVouchers.Add(detail);
                            //}
                            /////ends updated for accounting purpose
                        }
                    }
                    await _context.SaveChangesAsync();

                    foreach (var detail in Details)
                    {

                        detail.LedgerId = item.LedgerId;

                        //if shorttext is 'AmountEditable' means class fee is editable but didnt have anything entered.
                        if (detail.ShortText == "Empty" && detail.Reference == "NotAmountEditable")
                        {
                            detail.ShortText = "Receipt no. " + _StudentFeeReceipt.ReceiptNo;

                        }
                        if (reference == "")
                        {
                            reference = _StudentFeeReceipt.ReceiptNo.ToString();//detail.ShortText.Replace(" ", "").Substring(0, 10) + DateTime.Now.ToString("yyyyMMddHHmmss");
                        }

                        detail.Reference = reference;
                        if (detail.AccountingVoucherId == 0)
                        {
                            //adding two times. one for updating balance (reference) the other for receipt purpose
                            //detail.Month ==0 means discount, balance
                            //if (!_accountReceivableId.Select(o => o.GeneralLedgerId).ToList().Contains((int)detail.GeneralLedgerAccountId))
                            //{
                                detail.FeeReceiptId = 0;
                                //detail.MainItem = true;
                                //detail.GeneralLedgerAccountId = _studentLedgerId;
                                detail.AccountingVoucherId = 0;
                                detail.Debit = detail.Debit;
                                _context.AccountingVouchers.Add(detail);
                            //}
                            /////updated for accounting purpose
                            //else
                            //{
                            //    var discount = (decimal)detail.BaseAmount - detail.Amount;
                            //    if (discount > 0)
                            //    {
                            //        var objIndex = _accountReceivableId.Select(o => o.GeneralLedgerName).ToList().IndexOf("discount allowed");
                            //        if (objIndex > -1)
                            //        {
                            //            var discountAllowedId = _accountReceivableId[objIndex].GeneralLedgerId;
                            //            detail.FeeReceiptId = 0;
                            //            //detail.MainItem = true;
                            //            detail.GeneralLedgerAccountId = discountAllowedId;
                            //            detail.AccountingVoucherId = 0;
                            //            detail.Debit = true;
                            //            detail.Amount = discount;
                            //            _context.AccountingVouchers.Add(detail);
                            //        }
                            //    }
                            //    if(detail.Balance>0)
                            //    {
                            //        var objIndex = _accountReceivableId.Select(o => o.GeneralLedgerName).ToList().IndexOf("account receivable");
                            //        var accountReceivableId = _accountReceivableId[objIndex].GeneralLedgerId;
                            //        detail.FeeReceiptId = 0;
                            //        //detail.MainItem = true;
                            //        detail.GeneralLedgerAccountId = accountReceivableId;
                            //        detail.AccountingVoucherId = 0;
                            //        detail.Debit = true;
                            //        detail.Amount = detail.Balance;
                            //        _context.AccountingVouchers.Add(detail);
                            //    }
                            //}
                            /////ends updated for accounting purpose

                            var forreceipt = new AccountingVoucher()
                            {
                                LedgerId = detail.LedgerId,
                                //MainItem = false,
                                Month = detail.Month,
                                Active = 1,
                                AccountingVoucherId = 0,
                                Amount = detail.Amount,
                                BaseAmount = detail.BaseAmount,
                                Balance = detail.Balance,
                                ClassFeeId = detail.ClassFeeId,
                                Deleted = false,
                                FeeReceiptId = detail.Month == 0 ? 0 : _StudentFeeReceipt.StudentFeeReceiptId,
                                GeneralLedgerAccountId = detail.GeneralLedgerAccountId,
                                Debit = detail.Debit,
                                OrgId = detail.OrgId,
                                SubOrgId = detail.SubOrgId,
                                ShortText = detail.ShortText,
                                Reference = detail.Reference,
                                CreatedDate = DateTime.Now,
                                PostingDate = DateTime.Now,
                                DocDate = DateTime.Now,

                            };
                            _context.AccountingVouchers.Add(forreceipt);
                        }
                        else
                        {
                            detail.FeeReceiptId = _StudentFeeReceipt.StudentFeeReceiptId;
                            detail.GeneralLedgerAccountId = detail.GeneralLedgerAccountId;
                            detail.AccountingVoucherId = 0;
                            detail.Debit = detail.Debit;
                            //detail.MainItem = false;
                            _context.AccountingVouchers.Add(detail);
                        }
                        //not to be selected in the next accountdetailledger loop
                        if (detail.Month == 0)
                            detail.Month = item.Month;

                    }
                    await _context.SaveChangesAsync();

                }


                //automatic debit cash account
                //var _generalAccount = await _context.GeneralLedgers.Where(x => x.GeneralLedgerName.ToLower() == _ledgerName
                //&& x.OrgId == _StudentFeeReceipt.OrgId
                //&& x.SubOrgId == _StudentFeeReceipt.SubOrgId).FirstOrDefaultAsync();
                //ledgerid=0 means it is for an accounting purpose.
                //if (_generalLedgerAccountId > 0)
                //{
                //    var _cashAccountToAddToAccountingVoucher = new AccountingVoucher()
                //    {
                //        DocDate = DateTime.Now,
                //        PostingDate = DateTime.Now,
                //        BaseAmount = _StudentFeeReceipt.TotalAmount,
                //        Amount = _StudentFeeReceipt.TotalAmount,
                //        GeneralLedgerAccountId = _generalLedgerAccountId,// _generalAccount.GeneralLedgerId,
                //        Debit = _debit,
                //        Active = 1,
                //        Deleted = false,
                //        FeeReceiptId = _StudentFeeReceipt.StudentFeeReceiptId,
                //        ShortText = "Debit to " + _ledgerName,
                //        Reference = reference,
                //        OrgId = _StudentFeeReceipt.OrgId,
                //        SubOrgId = _StudentFeeReceipt.SubOrgId,
                //        LedgerId = 0,
                //        Balance = 0,

                //    };
                //    _context.AccountingVouchers.Add(_cashAccountToAddToAccountingVoucher);
                //}
                ////automatic credit tuition fee account
                //var _tuitionFeeAccount = await _context.GeneralLedgers.Where(x => x.GeneralLedgerName.ToLower() == "tuition fee"
                //&& x.OrgId == _StudentFeeReceipt.OrgId
                //&& x.SubOrgId == _StudentFeeReceipt.SubOrgId).FirstOrDefaultAsync();

                //var _tuitionFeeAccountToAddToAccountingVoucher = new AccountingVoucher()
                //{
                //    DocDate = DateTime.Now,
                //    PostingDate = DateTime.Now,
                //    BaseAmount = _StudentFeeReceipt.TotalAmount,
                //    Amount = _StudentFeeReceipt.TotalAmount,
                //    GeneralLedgerAccountId = _tuitionFeeAccount.GeneralLedgerId,
                //    Debit = false,
                //    Active = 1,
                //    Deleted = false,
                //    FeeReceiptId = _StudentFeeReceipt.StudentFeeReceiptId,
                //    Reference = reference,
                //    //Reference = "Receipt no. " + _StudentFeeReceipt.ReceiptNo,
                //    ShortText = "created to monthly fee",
                //    OrgId = _StudentFeeReceipt.OrgId,
                //    SubOrgId = _StudentFeeReceipt.SubOrgId,
                //    LedgerId = 0,
                //    Balance = 0,

                //};
                //_context.AccountingVouchers.Add(_tuitionFeeAccountToAddToAccountingVoucher);

                //only for TTP;
                //if(_StudentFeeReceipt.OrgId==1)
                //{
                //    var NoOfMonth = _AccountingVoucher.Where(x=>x.Month>0).Select(s=>s.Month).Distinct().Count();
                //    var org = await _context.Organizations.Where(x => x.OrganizationId == _StudentFeeReceipt.OrgId
                //    && x.SubOrgId == _StudentFeeReceipt.SubOrgId).Select(s => s).FirstOrDefaultAsync();

                //    org.ValidTo = Convert.ToDateTime(org.ValidTo).AddMonths(NoOfMonth);
                //    _context.Organizations.Update(org);

                //    _context.SaveChanges();

                //    var _admins = await _context.RoleUsers.Join(_context.MasterItems,
                //        roleuser => roleuser.RoleId,
                //        master => master.MasterDataId,
                //        (roleuser, master) => new { roleuser.Active, roleuser.OrgId, roleuser.UserId, master.MasterDataName })
                //        .Where(x => x.MasterDataName == "Admin"
                //        && x.OrgId == _StudentFeeReceipt.OrgId
                //        && x.Active == 1).Select(s => s).ToListAsync();

                //    foreach (var item in _admins)
                //    {
                //        var user = await _userManager.FindByIdAsync(item.UserId);
                //        user.ValidTo = Convert.ToDateTime(user.ValidTo).AddMonths(NoOfMonth);
                //        await _userManager.UpdateAsync(user);
                //    }
                //}
                await _context.SaveChangesAsync();
                /////////////////

                tran.Commit();
                return Ok(_StudentFeeReceipt);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                tran.Rollback();
                return BadRequest(ex);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/StudentFeeReceipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentFeeReceipt(int id)
        {
            var studentFeeReceipt = await _context.StudentFeeReceipts.FindAsync(id);
            if (studentFeeReceipt == null)
            {
                return NotFound();
            }

            _context.StudentFeeReceipts.Remove(studentFeeReceipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentFeeReceiptExists(int id)
        {
            return _context.StudentFeeReceipts.Any(e => e.StudentFeeReceiptId == id);
        }
    }
}
