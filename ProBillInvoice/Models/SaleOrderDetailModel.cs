using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleOrderDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
               
        [Display(Name = "Order Detail Id")]
        [Required(ErrorMessage = "Order Detail Id is required.")]
        public int order_detail_id { get; set; }

        [Display(Name = "Order No")]
        [Required(ErrorMessage = "Order Id is required.")]
        public int order_id { get; set; }
               
        [Display(Name = "Material")]
        [Required(ErrorMessage = "Material Id is required.")]
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Item Desc")]
        public string material_desc { get; set; }

        [Display(Name = "HSN Code")]
        public string hsn_code { get; set; }

        [Display(Name = "Unit")]
        [Required(ErrorMessage = "Unit Code is required.")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }

        [Display(Name = "Order Qty")]
        public decimal order_qty { get; set; }

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

        [Display(Name = "GST Included Rate")]
        public decimal final_item_rate { get; set; }

        [Display(Name = "Total gst")]
        public decimal totalgst { get; set; }

        [Display(Name = "Amount")]
        public decimal item_value { get; set; }

        public decimal total_iss_qty { get; set; }
        public bool is_pending { get; set; }
       public string  buyer_order_no { get; set; }
        public string party_name { get; set; }

        public string order_no { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime order_date { get; set; }
        public string material_code { get; set; }
        public string billing_address { get; set; }
        public string gst_no { get; set; }

        public string state_code { get; set; }
        public string state_name { get; set; }
        public decimal total_amount { get; set; }
    }
}