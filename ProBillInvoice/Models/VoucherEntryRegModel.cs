using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class VoucherEntryRegModel
    {       
        [Display(Name = "Account Name")]
        public int acct_id { get; set; }

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
        public int? voucher_id { get; set; }


        [Display(Name = "Voucher No")]       
        public string voucher_no { get; set; }

        [Display(Name = "Voucher Type")]        
        public string voucher_type { get; set; }

        [Display(Name = "Book Type")]
        public string book_type { get; set; }
                             
        [Display(Name = "Voucher Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> voucher_date { get; set; }

        [Display(Name = "Amount Type")]
        public string amount_type { get; set; }

        [Display(Name = "Account Name")]
        public int d_acct_id { get; set; }

        [Display(Name = "Opposite Account Name")]
        public int c_acct_id { get; set; }

        [Display(Name = "Op Bal")]
        public decimal opening_balance { get; set; }

        [Display(Name = "Debit")]
        public decimal debit { get; set; }

        [Display(Name = "Credit")]
        public decimal credit { get; set; }

        [Display(Name = "Closing Bal")]
        public decimal closing_balance { get; set; }

        [Display(Name = "Remark")]
        [StringLength(400, ErrorMessage = "Remarks must not be more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "Account Name")]
        public string account_name { get; set; }                   

        public IEnumerable<VoucherEntryRegModel> VoucherEntryRegList { get; set; }

        public Ad_CompanyMasterModel Company { get; set; }
    }
}