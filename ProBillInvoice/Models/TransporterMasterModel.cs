using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class TransporterMasterModel
    {

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Transporter Id")]
        [Required(ErrorMessage = "Transporter Id is required.")]
        public int transporter_id { get; set; }

        [Display(Name = "Account Code")]
        [Required(ErrorMessage = "Account code is required.")]
        public int acct_code { get; set; }

        [Display(Name = "Transporter Name")]
        [Required(ErrorMessage = "Transporter Name is required.")]
        public string transporter_name { get; set; }

        [Display(Name = "Billing Address")]
        [Required(ErrorMessage = "Billing Address is required.")]
        public string billing_address { get; set; }
        public int state_id { get; set; }

        [Display(Name = "Email")]
        public string emaiL_id { get; set; }
        public bool email_alert { get; set; }

        [Display(Name = "Mb No")]
        public string mobile_no { get; set; }
        public bool mobile_alert { get; set; }
        public decimal transporting_rate { get; set; }

        [Display(Name = "Opening Bal.")]
        [Required(ErrorMessage = "Opening Bal. is required.")]
        public decimal opening_balance { get; set; }
        public string amount_type { get; set; }
        public int godown_id { get; set; }
        public bool on_server { get; set; }
        public bool on_web { get; set; }

        public List<TransporterMasterModel> TransportList { get; set; }
    }
}