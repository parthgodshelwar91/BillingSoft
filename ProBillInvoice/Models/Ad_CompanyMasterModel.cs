using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class Ad_CompanyMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        //[Display(Name = "Sr No")]
        //public int sr_no { get; set; }

        [Display(Name = "Company Name")]
        public int company_id { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Company code field required")]
        [StringLength(10, ErrorMessage = "Company code must not be more than 10 char")]
        public string company_code { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name field required")]
        [StringLength(100, ErrorMessage = "Company name must not be more than 100 char")]
        public string company_name { get; set; }

        [Display(Name = "Short Name")]
        [Required(ErrorMessage = "Company short name field required")]
        [StringLength(10, ErrorMessage = "Short name must not be more than 10 char")]
        public string short_name { get; set; }

        [Display(Name = "Website")]
        public string website { get; set; }

        [Display(Name = "Registration No")]
        public string registration_no { get; set; }

        [Display(Name = "Email")]      
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Display(Name = "Billing Address")]
        [StringLength(150, ErrorMessage = "Billing address must not be more than 150 char")]
        public string billing_address { get; set; }

        [Display(Name = "Billing Address")]
        [StringLength(150, ErrorMessage = "Billing address must not be more than 150 char")]
        public string billing_address2 { get; set; }               

        [Display(Name = "Work Address")]
        [StringLength(150, ErrorMessage = "Work address must not be more than 150 char")]
        public string work_Address { get; set; }

        [Display(Name = "Work Address")]
        [StringLength(150, ErrorMessage = "Work address must not be more than 150 char")]
        public string work_Address2 { get; set; }

        [Display(Name = "Country Name")]
        public int country_id { get; set; }

        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "State name field required")]
        [StringLength(50, ErrorMessage = "Country name must not be more than 50 char")]
        public string country_name { get; set; }
        
        [Display(Name = "State Name")]
        public int state_id { get; set; }

        [Display(Name = "State Name")]
        [Required(ErrorMessage = "State name field required")]
        [StringLength(50, ErrorMessage = "State name must not be more than 50 char")]
        public string state_Name { get; set; }

        [Display(Name = "City Name")]
        public int city_id { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "City name field required")]
        [StringLength(50, ErrorMessage = "City name must not be more than 50 char")]
        public string city_name { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code field required")]
        [StringLength(6, ErrorMessage = "Zip Code must not be more than 6 char")]
        public string zip_code { get; set; }

        [Display(Name = "Logo")]
        public string logo_path { get; set; }

        [Display(Name = "Logo")]
        public string logo { get; set; }

        [Display(Name = "Auto Sign")]
        public string auto_sign { get; set; }

        [Display(Name = "Phone No")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "Zip Code must not be more than 10 Digit")]
        public string phone_no_one { get; set; }

        [Display(Name = "Fax No")]
        [StringLength(10, ErrorMessage = "Phone number must not be more than 10 char")]        
        public string phone_no_two { get; set; }

        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "Phone number must not be more than 10 char")]
        public string phone_no_three { get; set; }

        [Display(Name = "Branch Code")]
        public string branch_code { get; set; }

        [Display(Name = "Branch Name")]
        [StringLength(100, ErrorMessage = "Branch Name must not be more than 100 char")]
        public string branch_name { get; set; }

        [Display(Name = "Branch Bank Name")]
        [StringLength(100, ErrorMessage = "Branch Bank Name must not be more than 100 char")]
        public string branch_bank_name { get; set; }

        [Display(Name = "AC No")]
        public string bank_ac_no { get; set; }

        [Display(Name = "Bank Ac Name")]
        [StringLength(100, ErrorMessage = "Bank Ac Name must not be more than 100 char")]
        public string bank_ac_name { get; set; }

        [Display(Name = "IFSC Code")]
        public string IFSC { get; set; }

        [Display(Name = "MICR")]
        public string MICR { get; set; }

        [Display(Name = "CST No")]
        [StringLength(15, ErrorMessage = "CST No must not be more than 15 char")]
        //[RegularExpression(@"^\d{15}$", ErrorMessage = "CSTNO Number must be in 15 digits")]
        public string cst_no { get; set; }

        [Display(Name = "Valid Upto")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> cst_valid_upto { get; set; }

        [Display(Name = "CST Life Time")]
        public bool cst_lifetime { get; set; }

        [Display(Name = "CGC No")]
        [StringLength(15, ErrorMessage = "CGC No must not be more than 15 char")]
        //[RegularExpression(@"^\d{15}$", ErrorMessage = "CGC Number must be in 15 digits")]
        public string CGCTNo { get; set; }

        [Display(Name = "Valid Upto")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> CGCTValidityUpto { get; set; }

        [Display(Name = "CST Life Time")]
        public bool CGCTlifetime { get; set; }

        [Display(Name = "TIN No")]
        [StringLength(20, ErrorMessage = "TIN No must not be more than 20 char")]
        //[RegularExpression(@"^\d{15}$", ErrorMessage = "TIN No Number must be in 15 digits")]
        public string tin_no { get; set; }

        [Display(Name = "Valid Upto")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> tin_valid_upto { get; set; }

        [Display(Name = "Tin Life Time")]
        public bool tin_lifetime { get; set; }

        [Display(Name = "PAN No")]
        [StringLength(10, ErrorMessage = "PAN No must not be more than 10 char")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "PAN Number must be in 10 digits")]
        public string pan_no { get; set; }

        [Display(Name = "Valid Upto")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> pan_valid_upto { get; set; }

        [Display(Name = "PAN Life Time")]
        public bool pan_lifetime { get; set; }

        [Display(Name = "GST No")]
        [StringLength(20, ErrorMessage = "GST No must not be more than 20 char")]
        //[RegularExpression(@"^\d{20}$", ErrorMessage = "GST Number must be in 20 digits")]

        public string gst_no { get; set; }

        [Display(Name = "Aadhar No")]
        [StringLength(12, ErrorMessage = "Aadhar number must not be more than 12 char")]
        //[RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar Number must be in 12 digits")]
        public string aadhar_no { get; set; }

        [Display(Name = "CIN No")]
        [StringLength(20, ErrorMessage = "CIN number must not be more than 20 char")]
        public string cin_no { get; set; }

        [Display(Name = "Contact Person 1")]
        [StringLength(50, ErrorMessage = "Contact person must not be more than 50 char")]
        public string contact_person_one { get; set; }

        [Display(Name = "Designation 1")]
        [StringLength(50, ErrorMessage = "Designation must not be more than 50 char")]
        public string designation_one { get; set; }

        [Display(Name = "Contact No 1")]
        [StringLength(10, ErrorMessage = "Mobile number must not be more than 10 char")]
        public string contact_no_one { get; set; }

        [Display(Name = "C. Person 2")]
        [StringLength(50, ErrorMessage = "Contact person must not be more than 50 char")]
        public string contact_person_two { get; set; }

        [Display(Name = "Desig. 2")]
        [StringLength(50, ErrorMessage = "Designation must not be more than 50 char")]
        public string designation_two { get; set; }

        [Display(Name = "C. No 2")]
        [StringLength(10, ErrorMessage = "Mobile number must not be more than 10 char")]
        public string contact_no_two { get; set; }

        [Display(Name = "Top Head")]
        [StringLength(50, ErrorMessage = "Top header must not be more than 50 char")]
        public string top_head { get; set; }

        public string DB_Connection { get; set; }
        public bool CurrencySetting { get; set; }
        public Nullable<DateTime> installation_date { get; set; }
        public Nullable<DateTime> trial_expire_date { get; set; }
        public Nullable<DateTime> amc_expire_date { get; set; }

       

        public IEnumerable<Ad_CompanyMasterModel> CompanyMasterList { get; set; }
    }
}