using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class EmployeeMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Emp Id")]       
        public int emp_id { get; set; }

        [Display(Name = "Acct Id")]
        public int acct_id { get; set; }

        [Display(Name = "Emp Code")]
        public string emp_code { get; set; }

        [Display(Name = "Emp Category")]
        public string emp_type { get; set; }

        [Display(Name = "Emp Name")]
        [Required(ErrorMessage = "Party Name is required.")]
        [StringLength(50, ErrorMessage = "Party name must not be more than 50 char")]
        public string emp_name { get; set; }

        [Display(Name = "Emp Contact No")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone Number must be exactly 10 digits")]
        public string emp_contact_no { get; set; }        

        [Display(Name = "Emp Address")]
        public string emp_address { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Education")]
        public string education { get; set; }

        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_birth { get; set; }

        [Display(Name = "Age")]
        public int age { get; set; }

        [Display(Name = "is_date_of_joining")]
        public bool is_date_of_joining { get; set; }

        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_joining { get; set; }

        [Display(Name = "is_date_of_conformation")]
        public bool is_date_of_conformation { get; set; }

        [Display(Name = "Date Of Conformation")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_conformation { get; set; }

        [Display(Name = "is_date_of_leaving")]
        public bool is_date_of_leaving { get; set; }

        [Display(Name = "Date Of Leaving")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_leaving { get; set; }

        //[Display(Name = "Photo")]
        //public string photo { get; set; }

        [Display(Name = "Image")]
        public string image_path { get; set; }

        [Display(Name = "Department")]
        public int dept_id { get; set; }

        [Display(Name = "Site")]
        public int site_id { get; set; }

        [Display(Name = "Defunct")]
        public bool defunct { get; set; }

        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }

        public List<EmployeeMasterModel> EmployeeMasterList { get; set; }                        
    }
}