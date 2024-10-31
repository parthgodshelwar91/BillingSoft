using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class GrinHeaderModel
    {
        [Display(Name = "Site Name")]
        public int? SiteId { get; set; }

        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        [Display(Name = "Ticket")]
        public int ticket_number { get; set; }
        public decimal gross_weight { get; set; }
        public decimal tare_weight { get; set; }

        public decimal net_weight { get; set; }
        [Display(Name = "Order No")]
        public int order_id { get; set; }

        [Display(Name = "Party Name")]
        public string PartyName { get; set; }
        //------------------------------------------------------------------------------
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Grin Header Id")]
        public int grin_header_id { get; set; }

        [Display(Name = "GRN No")]
        public string grin_no { get; set; }

        [Display(Name = "GRN Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> grin_date { get; set; }

        [Display(Name = "GRN Type")]
        public string grin_type { get; set; }

        [Display(Name = "Gate No")]
        public string gate_no { get; set; }

        [Display(Name = "Gate Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> gate_date { get; set; }

        [Display(Name = "PO No")]
        public int? po_id { get; set; }

        [Display(Name = "PO No")]
        public string po_no { get; set; }

        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> po_date { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party name is required.")]
        public int party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Challan No")]
        public string cha_no { get; set; }

        [Display(Name = "Challan Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> cha_date { get; set; }

        [Display(Name = "Inv No")]
        public string inv_no { get; set; }

        [Display(Name = "Inv Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> inv_date { get; set; }

        [Display(Name = "Lr No")]
        public string lr_no { get; set; }

        [Display(Name = "Lr Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> lr_date { get; set; }

        [Display(Name = "Basic Amount")]
        public decimal basic_amount { get; set; }

        [Display(Name = "Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "total_rec_amount")]
        public decimal total_rec_amount { get; set; }

        [Display(Name = "Bill Status")]
        public bool grin_flag { get; set; }

        [Display(Name = "Payterm Code")]
        public int? payterm_code { get; set; }

        [Display(Name = "Payterm Days")]
        public string payterm_days { get; set; }

        [Display(Name = "Transporter")]
        [StringLength(100, ErrorMessage = "Transporter name must not be more than 100 char")]
        public string transporter { get; set; }

        [Display(Name = "Vehicle No")]
        [StringLength(10, ErrorMessage = "Transporter name must not be more than 10 char")]
        public string vehicle_no { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(400, ErrorMessage = "Remark more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "Site Name")]
        public int site_id { get; set; }

        [Display(Name = "Site Name")]
        public string site_name { get; set; }

        [Display(Name = "Company")]
        public int company_id { get; set; }      
        public string financial_year { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }    
        public string last_edited_by { get; set; }
        public Nullable<DateTime> last_edited_date { get; set; }

        //--------Report----------------------------------------------------------------------  
        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material Name")]
        public string material_name { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }

        [Display(Name = "Qty")]
        public decimal acce_qty { get; set; }

        [Display(Name = "Rate")]
        public decimal item_rate { get; set; }

       

        [Display(Name = "Sub Total")]
        public decimal sub_total { get; set; }

        [Display(Name = "Cgst")]
        public decimal cgst { get; set; }

        [Display(Name = "Sgst")]
        public decimal sgst { get; set; }

        [Display(Name = "Igst")]
        public decimal igst { get; set; }

        [Display(Name = "Amount")]
        public decimal item_value { get; set; }
        public string AmtInWord { get; set; }

    }
}