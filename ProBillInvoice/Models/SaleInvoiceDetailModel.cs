using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleInvoiceDetailModel
    {        
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Invoice Detail Id")]
        [Required(ErrorMessage = "Invoice Detail Id is required.")]
        public int invoice_detail_id { get; set; }

        [Display(Name = "Invoice Id")]
        [Required(ErrorMessage = "Invoice Id is required.")]
        public int sale_invoice_id { get; set; }

        [Display(Name = "Ticket Number")]
        [Required(ErrorMessage = "Ticket Number is required.")]
        public int ticket_number { get; set; }

        [Display(Name = "Ticket Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ticket_date_time { get; set; }

        [Display(Name = "Vehicle Number")]
        public string vehicle_number { get; set; }

        [Display(Name = "DM No")]
        public string dm_no { get; set; }
        public string slip_no { get; set; }
        public string concrete_type { get; set; }
        public int party_id { get; set; }
        public int material_id { get; set; }
        [Display(Name = "Material Name")]
        public string material_name { get; set; }
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }
        public int alt_unit { get; set; }
        public decimal con_factor { get; set; }
        public string long_desc { get; set; }
        public int location_id { get; set; }
        [Display(Name = "Location Name")]
        public string location_name { get; set; }
        public decimal stock_qty { get; set; }
        public decimal net_weight { get; set; }
        public decimal qty_in_cft { get; set; }
        public decimal item_qty { get; set; }
        public decimal pend_qty { get; set; }
        public decimal item_rate { get; set; }
        public decimal sub_total { get; set; }
        [Display(Name = "Cgst(%)")]
        public decimal cgst { get; set; }
        [Display(Name = "Cgst Amt")]
        public decimal cgst_amt { get; set; }
        [Display(Name = "Sgst(%)")]
        public decimal sgst { get; set; }
        [Display(Name = "Sgst Amt")]
        public decimal sgst_amt { get; set; }
        [Display(Name = "Igst Amt")]
        public decimal igst_amt { get; set; }
        [Display(Name = "Igst(%)")]
        public decimal igst { get; set; }
        public decimal item_value { get; set; }
        public decimal total_iss_qty { get; set; }
        public bool is_pending { get; set; }
        public bool is_select { get; set; }
        public string remarks { get; set; }
        public string financial_year { get; set; }
        public int site_id { get; set; }  

        [Display(Name = "Site")]
        public string site_name { get; set; }
        
        public int alt_unit_code { get; set; }
        public decimal alt_item_qty { get; set; }
        public decimal alt_item_rate { get; set; }

        //-----------------Using For Report--------------------------
        public string party_name { get; set; }
        public string billing_address { get; set; }

        public string invoice_no { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> invoice_date { get; set; }

        public string state_code { get; set; }
        public string state_name { get; set; }

        public decimal basic_amount { get; set; }
        public decimal total_amount { get; set; }
        public string gst_no { get; set; }

        public string material_code { get; set; }

        public string AmtInWord { get; set; }

        [Display(Name = "Item Desc")]
        public string material_desc { get; set; }

        [Display(Name = "HSN Code")]
        public string hsn_code { get; set; }

        [Display(Name = "GST Included Rate")]
        public decimal final_item_rate { get; set; }

        [Display(Name = "Total gst")]
        public decimal totalgst { get; set; }
        public decimal order_qty { get; set; }

        public int trips { get; set; }
    }
}