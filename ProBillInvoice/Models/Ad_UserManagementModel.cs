using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class Ad_UserManagementModel
    {
        [Display(Name = "User ID")]
        [Required(ErrorMessage = "User Id is required.")]
        public int userId { get; set; }

        [Display(Name = "sr_no")]        
        public int sr_no { get; set; }


        public string AspNetUserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(10, ErrorMessage = "User name should not be more than 10 char")]
        public string user_name { get; set; }

        [Display(Name = "Password")]
        //[Required(ErrorMessage = "Password is required.")]
        [StringLength(10, ErrorMessage = "Password should not be more than 10 char")]
        public string pass_word { get; set; }

        [Display(Name = "Retype Password")]
        //[Required(ErrorMessage = "Password R1 is required.")]
        [StringLength(10, ErrorMessage = "Password should not be more than 10 char")]
        public string pass_word_R1 { get; set; }

        [Display(Name = "Admin User")]
        public bool admin_user { get; set; }

        [Display(Name = "Primary User")]
        public bool primary_user { get; set; }

        [Display(Name = "Role")]
        //public int role_id { get; set; }
        public string AspNetRoleId { get; set; }

        [Display(Name = "Role")]
        public string role_name { get; set; }

        //[Required(ErrorMessage = "Please select Site Name Here.")]
        [Display(Name ="Site Name")]
        public int site_id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name must not be more than 50 char")]
        public string user_fname { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name must not be more than 50 char")]
        public string user_lname { get; set; }

        [Display(Name = "Job Title")]
        [StringLength(50, ErrorMessage = "Job Title must not be more than 50 char")]
        public string job_title { get; set; }

        //[Required(ErrorMessage = "Please select Department Name Here.")]
        [Display(Name = "Department")]
        public int dept_id { get; set; }

        [Display(Name = "Department")]
        public string dept_name { get; set; }

        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]       
        public string phone_no { get; set; }
        
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Display(Name = "Mobile Alerts")]
        public bool mobile_alerts { get; set; }

        [Display(Name = "Email Alerts")]
        public bool email_alerts { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime InactiveDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LoginIpAddress { get; set; }
        public bool IsLoggedIn { get; set; }
        public string default_fin_year { get; set; }
    }
}