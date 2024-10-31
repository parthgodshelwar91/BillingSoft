using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Models
{
    public class ProformaInvoiceHeaderModel
    {
        public string PartyName { get; set; }


        [Display(Name = "Mode")]
        public int Mode { get; set; }

        //[Display(Name = "Party Name")]
        //public int? PartyId { get; set; }
        public int? EnquiryId { get; set; }

        public int enquiry_id { get; set; }

        [Display(Name = "Proforma Invoice Id")]
        [Required(ErrorMessage = "Proforma Invoice id is required.")]
        public int proforma_invoice_id { get; set; }

        [Display(Name = "Invoice No")]
        [Required(ErrorMessage = "Invoice No is required.")]
        public string invoice_no { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Invoice Date")]
        public Nullable<DateTime> invoice_date { get; set; }

        [Display(Name = "Invoice Type")]
        public string invoice_type { get; set; }

        public int quotation_id { get; set; }

        //[Display(Name = "Party Name")]
        //public int enquiry_id { get; set; }
        public int party_id { get; set; }

        [Display(Name = "Sales Person")]
        public int sales_person_id { get; set; }

        [Display(Name = "Basic Amount")]
        public decimal basic_amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "Remark")]
        public string remarks { get; set; }

        [UIHint("tinymce_full"), AllowHtml]
        [Display(Name = "Term Codition")]
        public string term_codition { get; set; }

        [Display(Name = "mail_status")]
        public string mail_status { get; set; }

        [Display(Name = "whatsapp_status")]
        public string whatsapp_status { get; set; }

        public bool invoice_flag { get; set; }

        [Display(Name = "Site")]
        public int site_id { get; set; }

        public int company_id { get; set; }
        public string financial_year { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }
        public string last_edited_by { get; set; }
        public Nullable<DateTime> last_edited_date { get; set; }

        [Display(Name = "Party Name")]
        [Required(ErrorMessage = "Party Name is required.")]
        [StringLength(50, ErrorMessage = "Party name must not be more than 50 char")]
        public string party_name { get; set; }

        [Display(Name = "Quotation No")]
        public string quotation_no { get; set; }

        public string emp_name { get; set; }
        public string AmtInWord { get; set; }
        public string site_name { get; set; }

        public List<ProformaInvoiceHeaderModel> ProformaInvoiceList { get; set; }
    }
}