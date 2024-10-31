using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Models
{
    public class EnquiryFollowupModel
    {       
        public int party_id { get; set; }

        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Followup Id")]       
        public int followup_id { get; set; }
        
        [Display(Name = "Enquiry Id")]
        public int enquiry_id { get; set; }

        [Display(Name = "Visit No")]
        public int visit_no { get; set; }

        [Display(Name = "Followup Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> followup_date_time { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Contact Person")]
        [Required(ErrorMessage = "Contact Person is required.")]
        [StringLength(50, ErrorMessage = "Contact Person must not be more than 50 char")]
        public string contact_person { get; set; }

        [Display(Name = "Followup By")]
        public string followup_by { get; set; }

        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "Remarks is required.")]
        [StringLength(400, ErrorMessage = "Remarks must not be more than 400 Digit")]
        public string remarks { get; set; }

        [Display(Name = "Next Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Next Date is required.")]
        public Nullable<DateTime> next_followup_date { get; set; }
        
        [Display(Name = "Next Remark")]
       // [Required(ErrorMessage = "Next remark is required.")]
        [StringLength(400, ErrorMessage = "Remarks must not be more than 400 Digit")]
        public string next_followup_remarks { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }
        public string created_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> created_date { get; set; }

        public string last_edited_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public List<EnquiryFollowupModel> FollowupList { get; set; }

        public List<EnquiryDetailsModel> EnquiryDetailsList { get; set; }

    }
}