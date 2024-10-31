using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_BalanceSheetModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Group Id")]
        [Required(ErrorMessage = "Group Id is required.")]
        public int group_id { get; set; }

        [Display(Name = "Acct Id")]
        [Required(ErrorMessage = "Acct Id is required.")]
        public int acct_id { get; set; }

        [Display(Name = "Parent Id")]
        [Required(ErrorMessage = "Parent Id is required.")]
        public int parent_id { get; set; }

        [Display(Name = "Account Name")]
        public string account_name { get; set; }

        [Display(Name = "Amount Name")]
        public string amt_type { get; set; }

        [Display(Name = "Account Type")]
        public string acct_type { get; set; }

        [Display(Name = "Opening Balance")]
        public decimal opening_balance { get; set; }

        [Display(Name = "Debit")]
        public decimal debit { get; set; }

        [Display(Name = "Credit")]
        public decimal credit { get; set; }
        public decimal amount { get; set; }        
        public string amount_type { get; set; }       
        public string account_type { get; set; }
        public int account_level { get; set; }

        [Display(Name = "Debit Total")]
        public decimal debit_total { get; set; }

        [Display(Name = "Credit Total")]
        public decimal credit_total { get; set; }       
        public string head_type { get; set; }

        public List<Temp_BalanceSheetModel> Temp_BalanceSheet_CreditList { get; set; }
        public List<Temp_BalanceSheetModel> Temp_BalanceSheet_DebitList { get; set; }
    }
}