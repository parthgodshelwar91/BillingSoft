using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class VoucherEntryModel
    {
        [Display(Name = "PAYMENT")]
        public bool rbPayment { get; set; }

        [Display(Name = "RECEIPT")]
        public bool rbReceipt { get; set; }

        [Display(Name = "Account Name")]
        public int? CAcctId { get; set; }

        [Display(Name = "Voucher Type")]       
        public string VoucherType { get; set; }

        [Display(Name = "Voucher No")]       
        public string VoucherNo { get; set; }

        [Display(Name = "Same As")]
        public bool rbSameAs { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        //------------------------------------------------------------------------------
       
        [Display(Name = "Voucher Id")]
        [Required(ErrorMessage = "Voucher Id is required.")]
        public int? voucher_id { get; set; }

        [Display(Name = "Voucher Date")]
        [DataType(DataType.Date)]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]       
        public Nullable<DateTime> voucher_date { get; set; }               

        [Display(Name = "V Type")]
        public string v_type { get; set; }

        [Display(Name = "Voucher No")]
        //[Required(ErrorMessage = "Voucher Number is required.")]
        //[StringLength(20, ErrorMessage = "Voucher Number must not be more than 20 char")]
        public string voucher_no { get; set; }

        [Display(Name = "Voucher Type")]
        [Required(ErrorMessage = "Voucher Type is required.")]
        public string voucher_type { get; set; }

        [Display(Name = "Voucher Index")]
        public int voucher_index { get; set; }

        [Display(Name = "Book Type")]
        public string book_type { get; set; }
        public int ledger_folio { get; set; }

        [Display(Name = "Amount Type")]
        public string amount_type { get; set; }

        [Display(Name = "Account Name")]       
        public int dacct_id { get; set; }

        [Display(Name = "Opposite Account Name")]
        public int cacct_id { get; set; }

        [Display(Name = "Account Name")]
        [StringLength(50, ErrorMessage = "Account name must not be more than 50 char")]
        public string account_name { get; set; }

        [Display(Name = "Debit")]
        public decimal debit { get; set; }

        [Display(Name = "Credit")]
        public decimal credit { get; set; }

        [Display(Name = "Amount")]
        [Range(0.01, 999999999, ErrorMessage = "Please enter valid amount")]
        public decimal amount { get; set; }

        [Display(Name = "Payment Type")]
        public string payment_type { get; set; }

        [Display(Name = "Cheque No")]
        public string cheque_no { get; set; }

        [Display(Name = "Cheque Date")]       
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> cheque_date { get; set; }

        [Display(Name = "Branch Code")]
        public string branch_code { get; set; }

        [Display(Name = "Branch")]
        [StringLength(100, ErrorMessage = "Branch must not be more than 50 char")]
        public string branch_name { get; set; }

        [Display(Name = "Bank Name")]
        [StringLength(100, ErrorMessage = "Bank  Name must not be more than 100 char")]
        public string branch_bank_name { get; set; }

        [Display(Name = "Ac No")]
        public string bank_ac_no { get; set; }

        [Display(Name = "Bank Ac Name")]
        [StringLength(100, ErrorMessage = "Bank Ac Name must not be more than 100 char")]
        public string bank_ac_name { get; set; }

        [Display(Name = "IFSC Code")]
        [StringLength(11, ErrorMessage = "IFSC Code must not be more than 11 char")]
        public string IFSC { get; set; }
        public string MICR { get; set; }

        [Display(Name = "Remark")]
        [StringLength(200, ErrorMessage = "Remark must not be more than 200 char")]
        public string remarks { get; set; }

        [Display(Name = "Payment Type")]
        public string payment_mode_type { get; set; }

        public int id { get; set; }             
        public string financial_year { get; set; }
        public bool is_approved { get; set; }      
        public string last_edited_by { get; set; }      
        public DateTime last_edited_date { get; set; }

        public IEnumerable<VoucherEntryModel> VoucherEntryList { get; set; }

        public SaleInvoicePaymentModel SaleInvoiceModel { get; set; }

        public List<SaleInvoicePaymentModel> SaleInvoicePaymentList { get; set; }
        public List<GrinPaymentModel> GrinPaymentList { get; set; }
        public GrinPaymentModel GrinModel { get; set; }
    }
}