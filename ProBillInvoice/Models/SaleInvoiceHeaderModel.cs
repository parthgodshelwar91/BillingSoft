using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleInvoiceHeaderModel
    {
        [Display(Name = "Site")]
        public int? SiteId { get; set; }

        [Display(Name = "Company")]
        public int? CompanyId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Inv From")]
        public Nullable<DateTime> invoice_from { get; set; }

        [Display(Name = "Invoice To")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Date is required.")]
        public Nullable<DateTime> invoice_to { get; set; }

        [Display(Name = "Order No")]
        public int order_id { get; set; }

        [Display(Name = "Customer Location")]
        public int? customer_location_id { get; set; }

        //-----------------------------------------------------------------------------------
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Party Name")]        
        public int? PartyId { get; set; }          
        public int? location_id { get; set; }

        [Display(Name = "Inv Id")]
        public int sale_invoice_id { get; set; }

        [Display(Name = "Inv No")]
        public string invoice_no { get; set; }

        [Display(Name = "Inv Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> invoice_date { get; set; }

        [Display(Name = "Inv Type")]
        public string invoice_type { get; set; }

        [Display(Name = "SO No")]
        public int so_id { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party Name is required.")]
        public int party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Basic Amount")]
        public decimal basic_amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(400, ErrorMessage = "Remark more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "mail_status")]
        public string mail_status { get; set; }

        [Display(Name = "whatsapp_status")]
        public string whatsapp_status { get; set; }

        public bool invoice_flag { get; set; }

        [Display(Name = "Site")]
        public int site_id { get; set; }

        [Display(Name = "Company")]
        public int company_id { get; set; }

        public string financial_year { get; set; }

        [Display(Name = "Created By")]
        public string created_by { get; set; }

        [Display(Name = "Created Date")]
        public Nullable<DateTime> created_date { get; set; }

        [Display(Name = "Last Edited By")]
        public string last_edited_by { get; set; }

        [Display(Name = "Last Edited Date")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public string company_name { get; set; }
        public string company_code { get; set; }

        public string site_name { get; set; }

        public string AmtInWord { get; set; }


        //-----------------------------------------------------------------------------------
        //public string cha_no { get; set; }
        //public Nullable<DateTime> cha_date { get; set; }
        //public string inv_no { get; set; }
        //public Nullable<DateTime> inv_date { get; set; }
        //public string lr_no { get; set; }
        //public Nullable<DateTime> lr_date { get; set; }
        //public int payterm_id { get; set; }
        //public string transporter { get; set; }
        //public string vehicle_no { get; set; }
        //public string currency_type { get; set; }
        //public decimal currency_unit { get; set; }
        //public bool invoice_import { get; set; }
        //public string prepared_by { get; set; }
        //public Nullable<DateTime> actual_working_date { get; set; }
    }
}