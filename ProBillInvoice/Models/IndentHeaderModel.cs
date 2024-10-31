using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class IndentHeaderModel
    {
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]        
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }
        public string AmtInWord { get; set; }

        [Display(Name = "Indent Type")]
        public string IndType { get; set; }

        //------------------------------------------------------------------------------      
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Ind Header Id")]
        public int ind_header_id { get; set; }

        [Display(Name = "Indent No")]
        [Required(ErrorMessage = "Indent No is required.")]
        public string ind_no { get; set; }

        [Display(Name = "Indent Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ind_date { get; set; }

        [Display(Name = "Indent Type")]
        public string ind_type { get; set; }

        [Display(Name = "Dept Id")]
        public int? dept_id { get; set; }

        [Display(Name = "Department")]
        public string dept_name { get; set; }

        [Display(Name = "Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "Remark")]
        [StringLength(400, ErrorMessage ="Remark more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "Approved By")]
        public string approved_by { get; set; }

        [Display(Name = "Ind Approval")]
        public bool approval_flag { get; set; }

        [Display(Name = "Close Flag")]
        public bool close_flag { get; set; }

        [Display(Name = "Site Name")]
        public int? site_id { get; set; }

        [Display(Name = "Site Name")]
        public string site_name { get; set; }

        public int company_id { get; set; }
        public string financial_year { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }
        public string last_edited_by { get; set; }
        public Nullable<DateTime> last_edited_date { get; set; }
        public bool is_pending { get; set; }
        public string short_desc { get; set; }
        public decimal total_item_qty { get; set; }

        //--------Report----------------------------------------------------------------------  
        //[Display(Name = "Prepared By")]
        //public string prepared_by { get; set; }

        //[Display(Name = "Checked By")]
        //public string checked_by { get; set; }

        //[Display(Name = "Raised By")]
        //public string raised_by { get; set; }

        public int material_id { get; set; }

        [Display(Name = "Material Name")]
        public string material_name { get; set; }

        [Display(Name = "Rate")]
        public decimal item_qty { get; set; }

        [Display(Name = "Qty")]
        public decimal item_rate { get; set; }

        [Display(Name = "Amount")]
        public decimal item_value { get; set; }
        public string material_code { get; set; }
    }
}
