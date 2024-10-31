using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SubGroupMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        
        [Display(Name = "Gr Code")]
        [Required(ErrorMessage = "Please select group name from the list.")]
        public string group_code { get; set; }

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Please select group name from the list.")]
        public string group_name { get; set; }

        [Display(Name = "Sub Gr Code")]
        [Required(ErrorMessage = "Sub group code is required.")]
        [StringLength(4, ErrorMessage = "Sub group code must not be more than 4 char")]
        public string sub_group_code { get; set; }

        [Display(Name = "Sub Gr Desc")]
        [Required(ErrorMessage = "Sub group description is required.")]
        [StringLength(50, ErrorMessage = "Sub group Description must not be more than 50 char")]
        public string long_desc { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
           
        public string last_edited_by { get; set; }        
        public DateTime last_edited_date { get; set; }

        public List<SubGroupMasterModel> SubGroupMasterList { get; set; }
    }
}