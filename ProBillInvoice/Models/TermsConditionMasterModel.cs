using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class TermsConditionMasterModel
    {
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

        [Display(Name = "Terms Condition Id")]
        public int terms_conditions_id { get; set; }

        [Display(Name = "Used For")]
        public string used_for { get; set; }

        [Display(Name = "Terms Condition")]
        public string terms_conditions { get; set; }

        [Display(Name = "Company")]
        public int company_id { get; set; }

        [Display(Name = "Company")]
        public string company_name { get; set; }

        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }

        [Display(Name = "Last Edited By")]
        public string last_edited_by { get; set; }
        [Display(Name = "Document No")]
        public string doc_ref_no { get; set; }

        [Display(Name = "Last Edited Date")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public List<TermsConditionMasterModel> TermsConditionList { get; set; }
    }
}