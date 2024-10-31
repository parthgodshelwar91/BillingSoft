using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Models
{
    public class PurchaseDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Purchase Detail Id")]
        public int? purchase_detail_id { get; set; }

        [Display(Name = "Ind Header No")]
        public int? ind_header_id { get; set; }

        [Display(Name = "Ind No")]
        public string ind_no { get; set; }

        [Display(Name = "PO Id")]
        public int? po_id { get; set; } 

        [Display(Name = "Po No")]
        [Required(ErrorMessage = "Po No is required.")]
        public string po_no { get; set; }

        [Display(Name = "Material Id")]
        public int? material_id { get; set; }

        [Display(Name = "Item Name")]
        public string material_name { get; set; }

        [Display(Name = "Item Desc")]
        public string material_desc { get; set; }
        
        [Display(Name = "HSN Code")]
        public string hsn_code { get; set; }        

        [Display(Name = "Brand")]
        public int? brand_id { get; set; }

        [Display(Name = "Brand")]
        public string brand_name { get; set; }

        [Display(Name = "Unit")]
        public int? unit_code { get; set; }       

        [Display(Name = "Unit")]
        public string short_desc { get; set; }
        public int alt_unit { get; set; }

        [Display(Name = "Stock Qty")]
        public decimal stock_qty { get; set; }

        [Display(Name = "Item Qty")]
        public decimal item_qty { get; set; }

        [Display(Name = "Rate")]
        public decimal item_rate { get; set; }

        [Display(Name = "Discount")]
        public decimal discount { get; set; }

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

        [Display(Name = "Item Value")]
        public decimal item_value { get; set; }

        [Display(Name = "Total Rec Qty")]
        public decimal total_rec_qty { get; set; }

        public bool is_approved { get; set; }
        public bool is_pending { get; set; }
        public bool is_select { get; set; }

        [Display(Name = "Remark")]
        public string remarks { get; set; }

        public int? party_id { get; set; }

        [Display(Name = "GST Included Rate")]
        public decimal final_item_rate { get; set; }

        [Display(Name = "Total gst")]
        public decimal totalgst { get; set; }
        public decimal con_factor { get; set; }
        public IEnumerable<SelectListItem>MaterialName { get; set; }
        public IEnumerable<SelectListItem> BrandName { get; set; }
        public IEnumerable<SelectListItem> Short_descName { get; set; }
        public List<PurchaseDetailModel> PurchaseDetailList { get; set; }
    }
}