using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleOrderHeaderModel
    {
        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }

        public int? SiteId { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        public string PartyName { get; set; }

        //-----------------------------------------------------------------------------------
        [Display(Name = "Mode")]
        public int Mode { get; set; }                

        [Display(Name = "Order Id")]
        [Required(ErrorMessage = "Order Id is required.")]
        public int order_id { get; set; }

        [Display(Name = "Order No")]
        [Required(ErrorMessage = "Order No is required.")]
        public string order_no { get; set; }

        [Display(Name = " Buyer Order No")]
        [Required(ErrorMessage = "Buyer Order Id is required.")]
        public string buyer_order_no { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime order_date { get; set; }

        [Display(Name = "Order Type")]
        public string order_type { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Select Party Name from list.")]
        public int party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "City")]
        public int city_id { get; set; }

        [Display(Name = "City")]
        public string city_name { get; set; }

        public int location_id { get; set; }

        public int cust_site_location_id { get; set; }

        [Display(Name = "Cust Site Location")]
        public string location_detail { get; set; }

        public int acct_id { get; set; }
        public int broker_id { get; set; }

        public string account_name { get; set; }
        
        public decimal order_qty { get; set; }
        public decimal total_amount { get; set; }

        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> delivery_date { get; set; }  
        
        public string remarks { get; set; }
        public bool is_dispatched { get; set; }
        public bool in_schedule { get; set; }
        public bool order_close { get; set; }
        public int site_id { get; set; }

        public string site_name { get; set; }

        public int company_id { get; set; }          
        public string company_name { get; set; }
        public string financial_year { get; set; }
        
        [Display(Name = "Created By")]
        public string created_by { get; set; }

        [Display(Name = "Created Date")]
        public Nullable<DateTime> created_date { get; set; }

        [Display(Name = "Last Edited By")]
        public string last_edited_by { get; set; }

        [Display(Name = "Last Edited Date")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public int transporter_id { get; set; }
        public int payterm_id { get; set; }
        public string lr_no { get; set; }
        public Nullable<DateTime> lr_date { get; set; }        
        public string vehicle_number { get; set; }
        public string AmtInWord { get; set; }

        public string company_code { get; set; }
    }
}