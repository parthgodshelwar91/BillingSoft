using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleQuotationDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        public int sale_quotation_detail_id { get; set; }
        public int sale_quotation_header_id { get; set; }

        [Display(Name = "Material")]
        public int? material_id { get; set; }

        [Display(Name = "Material Name")]
        public string material_name { get; set; }
        public string material_desc { get; set; }

        [Display(Name = "Unit Code")]
        public int? unit_code { get; set; }
        public string short_desc { get; set; }
        public int alt_unit { get; set; }

        [Display(Name = "Net Wt")]
        public int net_weight { get; set; }

        [Display(Name = "In CFT")]
        public decimal qty_in_cft { get; set; }

        public decimal item_qty { get; set; }
        public decimal pend_qty { get; set; }

        [Display(Name = "Item Rate")]
        public decimal item_rate { get; set; }

        public decimal disc { get; set; }

        [Display(Name = "Sub Total")]
        public decimal sub_total { get; set; }

        [Display(Name = "Cgst(%)")]
        public decimal cgst { get; set; }
        [Display(Name = "Cgst Amt")]
        public decimal cgst_amt { get; set; }

        [Display(Name = "Sgst(%)")]
        public decimal sgst { get; set; }
        [Display(Name = "Sgst Amt")]
        public decimal sgst_amt { get; set; }

        [Display(Name = "Igst(%)")]
        public decimal igst { get; set; }

        [Display(Name = "Igst Amt")]
        public decimal igst_amt { get; set; }

        [Display(Name = "Amount")]
        public decimal item_value { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }

        public string financial_year { get; set; }

        [Display(Name = "Basic Rate")]
        public decimal basic_rate { get; set; }

        [Display(Name = "GST Included Rate")]
        public decimal final_item_rate { get; set; }

        [Display(Name = "Total gst")]
        public decimal totalgst { get; set; }
        public decimal con_factor { get; set; }
    }
}