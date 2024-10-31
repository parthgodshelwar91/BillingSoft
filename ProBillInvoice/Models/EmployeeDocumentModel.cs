using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class EmployeeDocumentModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Doc Id")]
        [Required(ErrorMessage = "Document Id is required.")]
        public int doc_id { get; set; }

        [Display(Name = "Doc No")]
        [Required(ErrorMessage = "Document No is required.")]
        [StringLength(10, ErrorMessage = "Document No must not be more than 10 char")]
        public string doc_no { get; set; }

        [Display(Name = "Doc Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> doc_date { get; set; }

        [Display(Name = "User Type")]
        [Required(ErrorMessage = "User Type is required.")]
        [StringLength(10, ErrorMessage = "User Type must not be more than 10 char")]
        public string user_type { get; set; }

        [Display(Name = "User Id")]
        [Required(ErrorMessage = "User Id is required.")]
        public int user_id { get; set; }

        [Display(Name = "Party Code")]
        [Required(ErrorMessage = "Party Code is required.")]
        [StringLength(10, ErrorMessage = "Party Code must not be more than 10 char")]
        public string partycode { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party Name is required.")]
        [StringLength(50, ErrorMessage = "Party name must not be more than 50 char")]
        public string partyname { get; set; }

        [Display(Name = "Emp Id")]
        [Required(ErrorMessage = "Party Id is required.")]
        public int emp_id { get; set; }

        [Display(Name = "Emp Name")]
        [Required(ErrorMessage = "Emp Name is required.")]
        [StringLength(50, ErrorMessage = "Emp name must not be more than 50 char")]
        public string emp_name { get; set; }

        [Display(Name = "Doc Type")]
        [Required(ErrorMessage = "Doc Type is required.")]
        [StringLength(10, ErrorMessage = "Doc Type must not be more than 10 char")]
        public string doc_type { get; set; }

        [Display(Name = "Doc Name")]
        public string doc_name { get; set; }

        [Display(Name = "Doc Description")]
        public string doc_discription { get; set; }

        [Display(Name = "Source Path")]
        public string source_path { get; set; }

        [Display(Name = "Destination Path")]
        public string destination_path { get; set; }

        [Display(Name = "Site")]
        public int site_id { get; set; }

        [Display(Name = "Defunct")]
        public bool defunct { get; set; }
              
        public int company_id { get; set; }
        public string financial_year { get; set; }

        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }

        public List<EmployeeDocumentModel> EmployeeDocumentList { get; set; }  
    }
}