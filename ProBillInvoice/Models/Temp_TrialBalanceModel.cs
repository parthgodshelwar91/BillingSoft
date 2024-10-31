using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class Temp_TrialBalanceModel
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

        [Display(Name = "Amount Type")]
        public string amt_type { get; set; }

        [Display(Name = "Account Type")]
        public string acct_type { get; set; }

        [Display(Name = "Opening Balance")]
        public decimal opening_balance { get; set; }

        [Display(Name = "Debit")]
        public decimal debit { get; set; }

        [Display(Name = "Debit Total")]
        public decimal debit_total { get; set; }

        [Display(Name = "Credit")]
        public decimal credit { get; set; }

        [Display(Name = "Credit Total")]
        public decimal credit_total { get; set; }

        public decimal amount { get; set; }       
        public string amount_type { get; set; }

        public List<Temp_TrialBalanceModel> Temp_TrialBalanceList { get; set; }
    }
}