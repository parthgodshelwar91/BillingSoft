using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class GrinPaymentModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "grin payment id")]
        public int grin_payment_id { get; set; }

        [Display(Name = "Grin Id")]
        public int grin_header_id { get; set; }

        public int voucher_id { get; set; }

        [Display(Name = "Inv No")]
        public string grin_no { get; set; }

        [Display(Name = "Inv Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> grin_date { get; set; }

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

        public bool grin_flag { get; set; }

        public bool defunct { get; set; }

        // public List<SaleInvoicePaymentModel> SaleInvoicePaymentList { get; set; }
        public List<GrinPaymentModel> GrinPaymentList { get; set; }
    
}
}