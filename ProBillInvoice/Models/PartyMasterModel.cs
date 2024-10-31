using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class PartyMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        [Display(Name = "sr_no")]
        public int Srsr_no { get; set; }

        [Display(Name = "Party Id")]
        [Required(ErrorMessage = "Party Id is required.")]
        public int party_id { get; set; }

        [Display(Name = "Party Code")]
        [Required(ErrorMessage = "Party Code is required.")]
        [StringLength(6, ErrorMessage = "Party Code must not be more than 3 char")]
        public string party_code { get; set; }

        [Display(Name = "Account Id")]
        public int acct_id { get; set; }

        [Display(Name = "Party Category")]
        public string party_category { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party Name is required.")]
        [StringLength(50, ErrorMessage = "Party name must not be more than 50 char")]
        public string party_name { get; set; }

        [Display(Name = "Billing Address")]
        [StringLength(400, ErrorMessage = "Billing address must not be more than 400 char")]
        public string billing_address { get; set; }

        [Display(Name = "Billing Address")]
        [StringLength(400, ErrorMessage = "Billing address must not be more than 400 char")]
        public string billing_address_1 { get; set; }

        [Display(Name = "Country")]
        public int country_id { get; set; }

        [Display(Name = "Country Name")]
        public string country_name { get; set; }

        [Display(Name = "State")]
        public int state_id { get; set; }

        [Display(Name = "State Name")]
        public string state_name { get; set; }

        [Display(Name = "City")]
        public int city_id { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "City is required.")]
        public string city_name { get; set; }

        [Display(Name = "Pin No")]
        [Required(ErrorMessage = "Pin No is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pin No. must be exactly 6 digits")]
        public string pin_no { get; set; }

        [Display(Name = "Phone No")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone Number must be exactly 10 digits")]
        public string phone_one { get; set; }

        [Display(Name = "Mb No")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number must be in 10 digits")]
        public string mobile_no { get; set; }       
        public string fax_no { get; set; }

        [Display(Name = "Contact Person")]
        [StringLength(50, ErrorMessage = "Contact person must not be more than 50 char")]
        public string contact_person { get; set; }

        [Display(Name = "Contact Mobile")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number must be in 10 digits")]
        public string contact_mobile_no { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Email must not be more than 50 char")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email format")]
        public string contact_email { get; set; }


        [Display(Name = "Delivery Address")]
        [StringLength(400, ErrorMessage = "Delivery address must not be more than 400 char")]
        public string delivery_address { get; set; }

        [Display(Name = "Delivery Address")]
        [StringLength(400, ErrorMessage = "Delivery address must not be more than 400 char")]
        public string delivery_address_1 { get; set; }

        [Display(Name = "Country")]
        public int country_id_1 { get; set; }

        [Display(Name = "Country Name")]
        public string country_name_1 { get; set; }

        [Display(Name = "State")]
        public int state_id_1 { get; set; }

        [Display(Name = "State Name")]
        public string state_name_1 { get; set; }

        [Display(Name = "City")]
        public int city_id_1 { get; set; }

        [Display(Name = "City Name")]
        public string city_name_1 { get; set; }

        [Display(Name = "Pin No")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pin No. must be exactly 6 digits")]
        public string pin_no_1 { get; set; }
        
        public bool email_alert { get; set; }
        public bool mobile_alert { get; set; }        
        public string ecc_no { get; set; }       
        public string cst_no { get; set; }        
        public string lst_no { get; set; }

        [Display(Name = "TIN No")]
        public string tin_no { get; set; }

        [Display(Name = "GST No")]
        [StringLength(15, ErrorMessage = "GST No. must be exactly 15 characters long.")]
        [RegularExpression(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9]{1}[Z]{1}[A-Z0-9]{1}$", ErrorMessage = "Invalid GST No. format.GST No Must be in the format Like 22AAAAA0000A1Z5.")]
        public string gst_no { get; set; }

        [Display(Name = "Aadhar No")]  
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Aadhar No must be in 12 digits.")]
        public string aadhar_no { get; set; }
        

        [Display(Name = "PAN No")]        
        [StringLength(10, ErrorMessage = "PAN No. must be exactly 10 characters long.")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN No. format.PAN No Must be in the format Like AFZPK7190K.")]
        public string pan_no { get; set; }

        [Display(Name = "Ex/Imp No")]
        public string export_import_no { get; set; }       
        public string direct_flag { get; set; }
        public bool import_party_flag { get; set; }

        [Display(Name = "Sale Person")]
        public int  sale_person_id { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
       public int type_id { get; set; }
        public bool critical_party_flag { get; set; }        
        public string critical_party_remark { get; set; }       
        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }
        public List<PartyMasterModel> CustomerList { get; set; }
    }
}