using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_AccountsCrDrModel
    {
        [Display(Name = "Account Name")]
        public int? AcctId { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        //------------------------------------------------------------------------------
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Acct Id")]        
        public int acct_id { get; set; }

        [Display(Name = "Account Name")]
        public string account_name { get; set; }

        [Display(Name = "Opening Balance")]
        public decimal opening_balance { get; set; }

        [Display(Name = "Debit")]
        public decimal debit { get; set; }

        [Display(Name = "Credit")]
        public decimal credit { get; set; }

        [Display(Name = "Closing Balance")]
        public decimal closing_balance { get; set; }    
        
        public string amount_type { get; set; }

        public List<Temp_AccountsCrDrModel> Temp_AccountsCrDrList { get; set; }
    }
}