using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class GrinTaxModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Tax Id")]      
        public int tax_id { get; set; }

        [Display(Name = "Grin Header Id")]       
        public int? grin_header_id { get; set; }
        
        [Display(Name = "Acct Id")]        
        public int? acct_id { get; set; }

        [Display(Name = "Acct Name")]
        public string acct_name { get; set; }

        [Display(Name = "Taxable Amount")]
        public decimal basic_amount { get; set; }

        [Display(Name = "Cgst(%)")]
        public decimal cgst { get; set; }

        [Display(Name = "Sgst(%)")]
        public decimal sgst { get; set; }

        [Display(Name = "Igst(%)")]
        public decimal igst { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal tax_amount { get; set; }

        public string account_name { get; set; }
    }
}