using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class ExecutiveMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Executive Id")]
        [Required(ErrorMessage = "Employee Id is required.")]
        public int emp_id { get; set; }

        [Display(Name = "Acct Id")]
        [Required(ErrorMessage = "Acct Id is required.")]
        public int acct_id { get; set; }

        [Display(Name = "Executive Code")]
        [Required(ErrorMessage = "Employee is required.")]
        public string emp_code { get; set; }

        [Display(Name = "Executive Type")]
        [Required(ErrorMessage = "Employee Type is required.")]
        public string emp_type { get; set; }

        [Display(Name = "Executive Name")]
        [Required(ErrorMessage = "Employee Name is required.")]
        [StringLength(50, ErrorMessage = "Employee name must not be more than 50 char")]
        public string emp_name { get; set; }

        [Display(Name = "Executive Address")]
        [Required(ErrorMessage = "Employee Address is required.")]
        [StringLength(100, ErrorMessage = "Employee Address must not be more than 100 char")]
        public string emp_address { get; set; }

        [Display(Name = "Mobile No1")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public string mobile_no { get; set; }

        [Display(Name = "Mobile No2")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public string mobile_no2 { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Email must not be more than 50 char")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Education")]
        public string education { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_birth { get; set; }
       
        public bool is_date_of_joining { get; set; }

        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_joining { get; set; }
        public bool is_date_of_leaving { get; set; }

        [Display(Name = "Date Of Leaving")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_leaving { get; set; }

        [Display(Name = "Document 1")]
        public string image_path { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }


        [Display(Name = "Document 2")]
        public string aadhar_card_path { get; set; }

        [Display(Name = "Document 3")]
        public string pan_card_path { get; set; }

        [Display(Name = "Document 4")]
        public string photo_path { get; set; }

        public List<ExecutiveMasterModel> CustomerList { get; set; }
    }
}