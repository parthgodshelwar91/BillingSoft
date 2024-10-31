using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class GrinDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Ticket Number")]
        public int? ticket_number { get; set; }

        [Display(Name = "Slip No")]
        public string slip_no { get; set; }

        [Display(Name = "Grin Detail Id")]        
        public int grin_detail_id { get; set; }

        [Display(Name = "Grin Header Id")]
        public int? grin_header_id { get; set; }

        [Display(Name = "Material")]
        public int? material_id { get; set; }

        public string material_name { get; set; }

        [Display(Name = "Brand")]
        public int? brand_id { get; set; }

        [Display(Name = "Site Name")]
        public int site_id { get; set; }

        [Display(Name = "Rack Id")]
        public int? rack_id { get; set; }

        [Display(Name = "Unit")]
        public int? unit_code { get; set; }
        public string short_desc { get; set; }
        public int alt_unit { get; set; }

        [Display(Name = "Mfg Date")]
        public string mfg_date { get; set; }

        [Display(Name = "PO Qty")]
        public decimal po_qty { get; set; }

        [Display(Name = "Rec Qty")]
        public decimal rece_qty { get; set; }

        [Display(Name = "Acce Qty")]
        public decimal acce_qty { get; set; }

        [Display(Name = "Rej Qty")]
        public decimal rej_qty { get; set; }

        public decimal pend_qty { get; set; }

        [Display(Name = "PO Rate")]
        public decimal po_item_rate { get; set; }

        [Display(Name = "Rate")]
        public decimal item_rate { get; set; }

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

        [Display(Name = "Remark")]
        [StringLength(400, ErrorMessage = "Remarks must not be more than 400 char")]
        public string remarks { get; set; }

        public bool is_select { get; set; }
        [Display(Name = "Material Code")]
        public string material_code { get; set; }

        [Display(Name = "Item Desc")]
        public string material_desc { get; set; }

        [Display(Name = "HSN Code")]
        public string hsn_code { get; set; }

        public decimal con_factor { get; set; }
    }
}