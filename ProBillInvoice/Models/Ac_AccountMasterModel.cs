using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class Ac_AccountMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Group Id")]
        [Required(ErrorMessage = "Group Id is required.")]
        public int group_id { get; set; }

        [Display(Name = "Acct Id")]
        [Required(ErrorMessage = "Acct Id is required.")]
        public int acct_id { get; set; }

        [Display(Name = "Under Group")]
        [Required(ErrorMessage = "Under Group is required.")]
        public int parent_id { get; set; }

        [Display(Name = "Under Group")]
        public string UnderGr { get; set; }

        public string group_code { get; set; }      
        public string acct_code { get; set; }      
        public string parent_code { get; set; }

        [Display(Name = "Account Name")]
        [Required(ErrorMessage = "Account Name is required.")]
        [StringLength(50, ErrorMessage = "Account name must not be more than 50 char")]
        public string account_name { get; set; }

        [Display(Name = "Account Header")]
        public string account_header { get; set; }

        [Display(Name = "Billing Address")]
        [Required(ErrorMessage = "Billing Address is required.")]
        [StringLength(100, ErrorMessage = "Billing address must not be more than 100 char")]
        public string billing_address { get; set; }

        [Display(Name = "Delivery Address")]
        [Required(ErrorMessage = "Delivery Address is required.")]
        [StringLength(100, ErrorMessage = "Delivery address must not be more than 100 char")]
        public string delivery_address { get; set; }

        [Display(Name = "Account Level")]
        public int account_level { get; set; }

        [Display(Name = "State Name")]
        public int state_id { get; set; }

        [Display(Name = "State Name")]
        public string state_name { get; set; }
        public int city_id { get; set; }

        [Display(Name = "City Name")]
        public string city_name { get; set; }

        [Display(Name = "Pin Code")]
        public string pin_code { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]
        public string emaiL_id { get; set; }

        [Display(Name = "Email Alert")]
        public bool email_alert { get; set; }

        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public string mobile_no { get; set; }

        [Display(Name = "Mobile Alert")]
        public bool mobile_alert { get; set; }

        [Display(Name = "GST No")]
        public string gst_no { get; set; }

        [Display(Name = "Aadhar No")]
        public string aadhar_no { get; set; }

        [Display(Name = "PAN No")]
        public string pan_no { get; set; }

        [Display(Name = "Tin No")]
        public string tin_no { get; set; }

        [Display(Name = "Sale Tax No")]
        public string sale_tax_no { get; set; }

        [Display(Name = "Balance Sheet")]
        public bool balance_sheet { get; set; }
        public bool trading { get; set; }
        public bool profit_loss { get; set; }

        [Display(Name = "Op Balance ")]
        public decimal amount { get; set; }

        [Display(Name = "Amount Type ")]
        public string amount_type { get; set; }

        [Display(Name = "Account Type ")]
        public string account_type { get; set; }

        [Display(Name = "Credit Limit ")]
        public decimal credit_limit { get; set; }

        [Display(Name = "Credit Days ")]        
        public decimal credit_days { get; set; }

        public bool sub_ledger { get; set; }

        [Display(Name = "Branch Code ")]
        public string branch_code { get; set; }

        [Display(Name = "Branch")]
        public string branch_name { get; set; }

        [Display(Name = "Bank Name")]
        public string branch_bank_name { get; set; }

        [Display(Name = "Ac No ")]
        public string bank_ac_no { get; set; }

        [Display(Name = "Bank Ac Name")]
        public string bank_ac_name { get; set; }

        [Display(Name = "IFSC Code")]
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public int company_id { get; set; }
        public int store_id { get; set; }
        public bool can_modify { get; set; }
        public bool defunct { get; set; }
        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }

        public List<Ac_AccountMasterModel> AccountMasterList { get; set; }
    }
}