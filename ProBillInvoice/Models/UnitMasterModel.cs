using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class UnitMasterModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Unit Code")]
        [Required(ErrorMessage = "Unit Code is required.")]
        public int unit_code { get; set; }

        [Display(Name = "Short Name")]
        [Required(ErrorMessage = "Short name is required.")]
        [StringLength(3, ErrorMessage = "Short name must not be more than 3 char")]
        public string short_desc { get; set; }

        [Display(Name = "Unit Description")]
        [Required(ErrorMessage = "Unit Description is required.")]
        [StringLength(50, ErrorMessage = "Unit description must not be more than 50 char")]
        public string long_desc { get; set; }

        [Display(Name = "Defunct")]
        public bool defunct { get; set; }

        [Display(Name = "Last Edited By")]
        public string last_edited_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Last Edited Date:")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public List<UnitMasterModel> StoreUOMList { get; set; }
    }
}


// productMaster --Sale