using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Models
{
    public class ProformaInvoiceDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Invoice Detail Id")]
        [Required(ErrorMessage = "Invoice Detail id is required.")]
        public int invoice_detail_id { get; set; }

        [Display(Name = "Proforma Invoice Id")]
        [Required(ErrorMessage = "Proforma Invoice id is required.")]
        public int proforma_invoice_id { get; set; }

        [Display(Name = "Material Id")]      
        public int material_id { get; set; }

        [Display(Name = "Unit Code")]    
        public int unit_code { get; set; }
        public string short_desc { get; set; }
        public int alt_unit { get; set; }

        [Display(Name = "Item Description")]
        public string material_desc { get; set; }
        public string material_name { get; set; }

        [Display(Name = "Net Wt")]
        public decimal net_weight { get; set; }

        [Display(Name = "Qty")]
        public decimal qty_in_cft { get; set; }

        [Display(Name = "Item Qty")]
        public decimal item_qty { get; set; }


        public decimal pend_qty { get; set; }

        [Display(Name = "Rate")]
        public decimal item_rate { get; set; }
       
        public decimal disc { get; set; }

        [Display(Name = "Sub Total")]
        public decimal sub_total { get; set; }

        [Display(Name = "Ggst")]
        public decimal cgst { get; set; }
        [Display(Name = "Cgst Amt")]
        public decimal cgst_amt { get; set; }

        [Display(Name = "Sgst")]
        public decimal sgst { get; set; }
        [Display(Name = "Sgst Amt")]
        public decimal sgst_amt { get; set; }

        [Display(Name = "Igst")]
        public decimal igst { get; set; }
        [Display(Name = "Igst Amt")]
        public decimal igst_amt { get; set; }

        [Display(Name = "Amount")]
        public decimal item_value { get; set; }

        [Display(Name = "Remark")]
        public string remarks { get; set; }

        public string financial_year { get; set; }
        [Display(Name = "GST Included Rate")]
        public decimal final_item_rate { get; set; }

        [Display(Name = "Total gst")]
        public decimal totalgst { get; set; }
        public string hsn_code { get; set; }
        public decimal con_factor { get; set; }

        //public IEnumerable<SelectListItem> material_name { get; set; }
        //public IEnumerable<SelectListItem> short_desc { get; set; }
        //material_name { get; set; }
    }
}