using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class PurchaseTaxModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Tax Id")]
        public int? tax_id { get; set; }

        [Display(Name = "PO Id")]
        public int? po_id { get; set; }

        [Display(Name = "Taxation")]
        public int? acct_id { get; set; }

        [Display(Name = "Taxable Amount")]
        public decimal basic_amount { get; set; }

        [Display(Name = "Cgst(%)")]
        public decimal cgst { get; set; }

        [Display(Name = "Sgst(%)")]
        public decimal sgst { get; set; }

        [Display(Name = "Igst(%)")]
        public decimal igst { get; set; }

        [Display(Name = "Tax Amt")]
        public decimal tax_amount { get; set; }     
        public string account_name { get; set; }

        public List<PurchaseTaxModel> PurchaseTaxList { get; set; }
    }
}