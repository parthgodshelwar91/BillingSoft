using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleInvoiceTaxesModel
    {        
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Tax Id")]
        [Required(ErrorMessage = "Tax  Id is required.")]
        public int tax_id { get; set; }

        [Display(Name = "Sale Invoice Id")]
        [Required(ErrorMessage = "Sale Invoice Id is required.")]
        public int sale_invoice_id { get; set; }

        [Display(Name = "Account Id")]
        public int? acct_id { get; set; }
        [Display(Name = "Account Name")]
        public string account_name { get; set; }
        

        [Display(Name = "Account Code")]
        public string acct_code { get; set; }
        public decimal percentage { get; set; }
        public decimal cgst { get; set; }
        public decimal sgst { get; set; }
        public decimal igst { get; set; }
        public decimal amount { get; set; }
        public string financial_year { get; set; }

        
    }
}