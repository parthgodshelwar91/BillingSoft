using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class MaterialMasterModel
    {

        [Display(Name = "Sr. No.")]
        public int sr_no { get; set; }

        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Material")]
        [Required(ErrorMessage = "Material id is required.")]
        public int material_id { get; set; }

        [Display(Name = "Material Code")]
        public string material_code { get; set; }

        [Display(Name = "Group Code")]
        public string group_code { get; set; }

        [Display(Name = "Group Name")]
        public string gr_name { get; set; }

        [Display(Name = "Sub Gr Code")]
        public string sub_group_code { get; set; }

        [Display(Name = "Sub Group Name")]
        public string sub_gr_name { get; set; }

        [Display(Name = "S S Gr Code")]
        public string sub_sub_group_code { get; set; }

        [Display(Name = "Sub Sub Group Name")]
        public string sub_sub_gr_name { get; set; }

        [Display(Name = "HSN Code")]
        [StringLength(10, ErrorMessage = "HSN Code must not be more than 10 char")]
        public string hsn_code { get; set; }

        [Display(Name = "Material Type")]
        [Required(ErrorMessage = "Material type is required.")]
        public string material_type { get; set; }

        [Display(Name = "Material Name")]
        [Required(ErrorMessage = "Material name is required.")]
        [StringLength(50, ErrorMessage = "Material name must not be more than 50 char")]
        public string material_name { get; set; }
        public string material_desc { get; set; }

        [Display(Name = "Recipe Name")]
        //[Required(ErrorMessage = "Recipe name is required.")]
        //[StringLength(30, ErrorMessage = "Recipe name must not be more than 50 char")]
        public string material_recipe_name { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }

        [Display(Name = "Alt Unit")]
        public int alt_unit { get; set; }

        [Display(Name = "Con Factor")]
        public decimal con_factor { get; set; }
               
        [Display(Name = "Material Rate")]
        public decimal material_rate { get; set; }

        [Display(Name = "CGST (%)")]
        public decimal cgst { get; set; }

        [Display(Name = "SGST (%)")]
        public decimal sgst { get; set; }

        [Display(Name = "IGST (%)")]
        public decimal igst { get; set; }

        [Display(Name = "Stock Posting")]
        public bool stock_posting { get; set; }

        [Display(Name = "Scrap")]
        public bool is_scrap { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }

        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }
        public string last_edited_by { get; set; }
        public Nullable<DateTime> last_edited_date { get; set; }
       
        public List<MaterialMasterModel> MaterialMasterList { get; set; }
    }
}