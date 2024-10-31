using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class EmployeeNomineeModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Nominee Id")]
        [Required(ErrorMessage = "Nominee Id is required.")]
        public int nominee_id { get; set; }

        [Display(Name = "Emp Id")]
        [Required(ErrorMessage = "Emp Id is required.")]
        public int emp_id { get; set; }

        [Display(Name = "Nominee Name")]
        [Required(ErrorMessage = "Nominee Name is required.")]
        [StringLength(50, ErrorMessage = "Nominee name must not be more than 50 char")]
        public string nominee_name { get; set; }

        [Display(Name = "Relation")]
        public string emp_relation { get; set; }

        [Display(Name = "Mb No")]
        public string contact_no { get; set; }

        [Display(Name = "Aadhar No")]
        public string aadhar_no { get; set; }

        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_of_birth { get; set; }

        [Display(Name = "Remark")]
        public string remark { get; set; }

        [Display(Name = "Enclosement")]
        public string encl { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date { get; set; }

        [Display(Name = "Place")]
        public string place { get; set; }

        public List<EmployeeNomineeModel> EmployeeNomineeList { get; set; }
    }
}