using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class IndentDetailModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Ind Detail Id")]
        [Required(ErrorMessage = "Ind Detail Id is required.")]
        public int ind_detail_id { get; set; }

        [Display(Name = "Ind Header Id")]
        [Required(ErrorMessage = "Ind Header Id is required.")]
        public int ind_header_id { get; set; }

        [Display(Name = "Indent No")]
        public string ind_no { get; set; }

        [Display(Name = "Indent Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]       
        public Nullable<DateTime> ind_date { get; set; }

        [Display(Name = "Material")]
        [Required(ErrorMessage = "Material is required.")]       
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Item Desc")]
        public string material_desc { get; set; }

        [Display(Name = "Brand")]
        public int brand_id { get; set; }

        [Display(Name = "Brand")]
        public string brand_name { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }
      
        [Display(Name = "Unit")]
        public string short_desc { get; set; }
        public int alt_unit { get; set; }

        [Display(Name = "Machine Id")]
        public int machine_id { get; set; }

        [Display(Name = "Stock Qty")]
        public decimal item_stock_qty { get; set; }

        [Display(Name = "Item Qty")]
        public decimal item_qty { get; set; }

        public decimal req_qty { get; set; }

        [Display(Name = "Item Rate")]
        public decimal item_rate { get; set; }

        [Display(Name = "Item Amount")]
        public decimal item_value { get; set; }

        [Display(Name = "Total Item Qty")]
        public decimal total_item_qty { get; set; }

        [Display(Name = "Req Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> required_date { get; set; }

        [Display(Name = "Emp Name")]
        public string emp_name { get; set; }

        [Display(Name = "Emp Dept")]
        public string emp_dept { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(400, ErrorMessage = "Remark more than 400 char")]
        public string remarks { get; set; }

        [Display(Name = "Is Pending")]
        public bool is_pending { get; set; }

        [Display(Name = "Approved")]
        public bool is_approved { get; set; }

        [Display(Name = "Approved Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> approved_date { get; set; }

        [Display(Name = "Approved Remarks")]
        [Required(ErrorMessage = "Approved Remark is required.")]
        [StringLength(400, ErrorMessage = "Approved Remark must not be more than 75 char")]
        public string approved_remarks { get; set; }

        [Display(Name = "Cancel")]
        public bool is_cancel { get; set; }

        [Display(Name = "Cancel Remarks")]
        [Required(ErrorMessage = "Cancel Remark is required.")]
        [StringLength(400, ErrorMessage = "Cancel Remark must not be more than 75 char")]
        public string cancel_remarks { get; set; }

        public decimal con_factor { get; set; }
    }
}
