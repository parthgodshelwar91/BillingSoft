using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleQuotationHeaderModel
    {
        [Display(Name = "Party Name")]
        public string PartyName { get; set; }

        [Display(Name = "Mode")]
        public int Mode { get; set; }
        //public int? PartyId { get; set; }
        public int? EnquiryId { get; set; }
        public int sale_quotation_header_id { get; set; }

        [Display(Name = "Quotation No")]
        public string quotation_no { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> quotation_date { get; set; }

        [Display(Name = "Quotation Type")]
        public string quotation_type { get; set; }

        [Display(Name = "Party Name")]
        public int enquiry_id { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party name is required.")]
        public string party_name { get; set; }

        [Display(Name = "Sales Person")]
        public int sales_person_id { get; set; }

        [Display(Name = "Sales Person")]
        public string employee_name { get; set; }

        [Display(Name = "Ref No")]
        public string ref_no { get; set; }

        [Display(Name = "Subject")]
        public string quotation_subject { get; set; }
       
        public string quotation_kindAttn { get; set; }
       
        public string quotation_body { get; set; }
       
        public string cement_brand { get; set; }

        [Display(Name = "Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(400, ErrorMessage = "Remarks must not be more than 400 Digit")]
        public string remarks { get; set; }

        [Display(Name = "Terms Coditions")]
        public string terms_coditions { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> quotation_expiry_date { get; set; }

        [Display(Name = "mail_status")]
        public string mail_status { get; set; }

        [Display(Name = "whatsapp_status")]
        public string whatsapp_status { get; set; }

        [Display(Name = "Status")]
        public bool approval_flag { get; set; }
        public int site_id { get; set; }      
        public int company_id { get; set; }     
        public string financial_year { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }
        public string last_edited_by { get; set; }
        public Nullable<DateTime> last_edited_date { get; set; }
        public string AmtInWord { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }


    }
}