using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class EnquiryDetailsModel
    {       
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Enquiry Id")]
        public int enquiry_id { get; set; }
        [Display(Name = "Enquiry No")]
        public string enquiry_no { get; set; }

        [Display(Name = "Enquiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public Nullable<DateTime> enquiry_date { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party name is required.")]
        [StringLength(50, ErrorMessage = "Party name must not be more than 50 char")]
        public string party_name { get; set; }

        [Display(Name = "Site Detail")]
        [Required(ErrorMessage = "Site Detail is required.")]
        [StringLength(70, ErrorMessage = "Site Detail must not be more than 70 char")]
        public string site_detail { get; set; }

        [Display(Name = "Address")]
        [StringLength(400, ErrorMessage = "Billing address must not be more than 400 char")]
        public string billing_address { get; set; }

        [Display(Name = "Address")]
        [StringLength(400, ErrorMessage = "Billing address must not be more than 400 char")]
        public string billing_address_1 { get; set; }

        [Display(Name = "State Name")]
        [Required(ErrorMessage = "State name is required.")]
        public int state_id { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "City name is required.")]
        public int city_id { get; set; }

        [Display(Name = "Contact Person")]
        [StringLength(50, ErrorMessage = "Contact Person must not be more than 50 char")]
        public string contact_person { get; set; }

        [Display(Name = "Mobile no")]
        [Required(ErrorMessage = "Mobile number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public string mobile_no { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Display(Name = "GST No")]
        [StringLength(15, ErrorMessage = "GST No. must be exactly 15 characters long.")]
        [RegularExpression(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9]{1}[Z]{1}[A-Z0-9]{1}$", ErrorMessage = "Invalid GST No. format.GST No Must be in the format Like 22AAAAA0000A1Z5.")]
        public string gst_no { get; set; }

        [Display(Name = "Interested In")]
        public string interested_in { get; set; }

        [Display(Name = "Enq Sorurce")]
        public string category { get; set; }

        [Display(Name = "Enquiry By")]
        public string enquiry_by { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(400, ErrorMessage = "Remarks must not be more than 400 Digit")]
        public string remarks { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required.")]
        public string enquiry_status { get; set; }

        public string created_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> created_date { get; set; }

        public string last_edited_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public string city_name { get; set; }
        public string state_name { get; set; }

        [Display(Name = "Emp Name")]
        public string emp_name { get; set; }

        public int emp_id { get; set; }

        [Display(Name = "Pin No")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pin No. must be exactly 6 digits")]
        public string pin_no { get; set; }

        public List<EnquiryDetailsModel> EnquiryDetailsList { get; set; }

        public EnquiryDetailsModel EnquiryDetails { get; set; }

        public List<EnquiryFollowupModel> FollowupList { get; set; }

    }
}