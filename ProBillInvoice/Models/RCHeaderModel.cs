using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class RCHeaderModel
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

        //------------------------------------------------------------------------------
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "rc_header_id")]
        public int rc_header_id { get; set; }

        [Display(Name = "RC No")]
        public string rc_no { get; set; }

        [Display(Name = "RC Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> rc_date { get; set; }

        [Display(Name = "RC Type")]
        public string rc_type { get; set; }

        [Display(Name = "Grin Header Id")]
        public int grin_header_id { get; set; }

        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Supplier name is required.")]
        public int party_id { get; set; }

        [Display(Name = "Supplier Name")]
        public string party_name { get; set; }

        [Display(Name = "Gate No")]
        public string gate_no { get; set; }

        [Display(Name = "Gate Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> gate_date { get; set; }

        [Display(Name = "Challan No")]
        public string cha_no { get; set; }

        [Display(Name = "Challan Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> cha_date { get; set; }

        [Display(Name = "Freight to Paid")]
        public decimal fre_bank { get; set; }

        [Display(Name = "Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(400, ErrorMessage = "Remark more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "Transporter")]
        [StringLength(100, ErrorMessage = "Transporter name must not be more than 100 char")]
        public string transporter { get; set; }

        [Display(Name = "Vehicle No")]
        [StringLength(10, ErrorMessage = "Transporter name must not be more than 10 char")]
        public string vehicle_no { get; set; }

        [Display(Name = "RC Flag")]
        public bool rc_flag { get; set; }

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

        public string AmtInWord { get; set; }
    }
}