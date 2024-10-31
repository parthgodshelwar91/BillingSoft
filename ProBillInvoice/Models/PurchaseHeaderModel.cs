using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class PurchaseHeaderModel
    {
        [Display(Name = "Site Name")]
        public int? SiteId { get; set; }

        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }

        [Display(Name = "PO No")]
        public int? PoNo { get; set; }

        [Display(Name = "Type")]
        public string PoType { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        //------------------------------------------------------------------------------        
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "PO Id")]
        [Required(ErrorMessage = "Po Id is required.")]
        public int? po_id { get; set; }

        [Display(Name = "PO No")]
        [Required(ErrorMessage = "Po No is required.")]
        public string po_no { get; set; }

        [Display(Name = "PO Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> po_date { get; set; }

        [Display(Name = "Type")]
        public string po_type { get; set; }       

        [Display(Name = "Party Name")]
        //[Required(ErrorMessage = "Select Party Name from list.")]
        public int? party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Basic Amount")]
        public decimal basic_amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal total_amount { get; set; }       

        [Display(Name = "Payterm Code")]
        public int? payterm_code { get; set; }

        [Display(Name = "Payterm Desc")]
        public string payterm_desc { get; set; }

        [Display(Name = "Payterm Days")]
        public string payterm_days { get; set; }               

        [Display(Name = "Deliver At Site")]
        public int del_site_id { get; set; }

        [Display(Name = "Deliver At Site")]
        public string deliver_site { get; set; }
        
        [Display(Name = "Po Validility Date")] //Del Completion Date
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> del_completion_date { get; set; }

        [Display(Name = "Transportation")]
        [StringLength(100, ErrorMessage = "Transportation must not be more than 100 char")]
        public string transportation { get; set; }

        [Display(Name = "Remark")]
        [StringLength(400, ErrorMessage = "Remark more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "PO Import")]
        public bool po_import { get; set; }

        [Display(Name = "Currency Type")]
        public string currency_type { get; set; }

        [Display(Name = "Currency Unit")]
        public decimal currency_unit { get; set; }

        [Display(Name = "Po Approval")]
        public bool approval_flag { get; set; }

        [Display(Name = "Po Close Flag")]
        public bool po_close_flag { get; set; }

        [Display(Name = "Site Name")]
        public int site_id { get; set; }

        [Display(Name = "Site Name")]
        public string site_name { get; set; }  

        [Display(Name = "Company")]
        //[Required(ErrorMessage = "Company Name is required.")]
        public int? company_id { get; set; }
        [Display(Name = "Company")]
        public string company_name { get; set; }
        public string financial_year { get; set; }

        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }

        public decimal GstAmt { get; set; }

        public decimal BasicAmt { get; set; }
        public decimal SubTotalAmt { get; set; }

        //--------Report----------------------------------------------------------------------  
        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material Name")]
        public string material_name { get; set; }

        [Display(Name = "Qty")]
        public decimal item_qty { get; set; }

        [Display(Name = "Rate")]
        public decimal item_rate { get; set; }

        [Display(Name = "Discount")]
        public decimal discount { get; set; }

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
        public string company_code { get; set; }
        public string short_desc { get; set; }
        public decimal total_rec_qty { get; set; }
        public bool is_pending { get; set; }
        
        public List<PurchaseHeaderModel> PurchaseHeaderList { get; set; }
    }
}