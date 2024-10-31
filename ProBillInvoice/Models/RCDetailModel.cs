using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class RCDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "rc_detail_id")]
        public int rc_detail_id { get; set; }

        [Display(Name = "rc_header_id")]
        public int rc_header_id { get; set; }

        [Display(Name = "Grin Header Id")]
        public int grin_header_id { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party name is required.")]
        public int party_id { get; set; }

        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Item Desc")]
        public string material_desc { get; set; }

        [Display(Name = "HSN Code")]
        public string hsn_code { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }
                
        [Display(Name = "Brand")]
        public int brand_id { get; set; }

        [Display(Name = "Site Name")]
        public int site_id { get; set; }

        [Display(Name = "Rack Id")]
        public int rack_id { get; set; }

        [Display(Name = "Stock Qty")]
        public decimal stock_qty { get; set; }

        [Display(Name = "Acce Qty")]
        public decimal acce_qty { get; set; }

        [Display(Name = "Rej Qty")]
        public decimal rej_qty { get; set; }

        [Display(Name = "Item Rate")]
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
    }
}