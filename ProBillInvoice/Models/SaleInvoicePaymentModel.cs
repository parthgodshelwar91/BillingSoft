using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class SaleInvoicePaymentModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "sale_invoice_payment_id")]
        public int sale_invoice_payment_id { get; set; }

        [Display(Name = "Inv Id")]
        public int sale_invoice_id { get; set; }

        public int voucher_id { get; set; }

        [Display(Name = "Inv No")]
        public string invoice_no { get; set; }

        [Display(Name = "Inv Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> invoice_date { get; set; }

        [Display(Name = "Customer Name")]
        public int party_id { get; set; }

        [Display(Name = "Customer Name")]
        public string party_name { get; set; }

        [Display(Name = "TDS Amount")]
        public decimal tds_amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal total_rec_amount { get; set; }

        public bool invoice_flag { get; set; }
       
        public bool defunct { get; set; }

       // public List<SaleInvoicePaymentModel> SaleInvoicePaymentList { get; set; }
        public List<SaleInvoicePaymentModel> SaleInvoicePaymentList { get; set; }
    }
}