using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SubSubGroupMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        
        [Display(Name = "Gr Code")]
        [Required(ErrorMessage = "Group code is required.")]
        [StringLength(2, ErrorMessage = "Group code must not be more than 2 char")]
        public string group_code { get; set; }

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Please select group name from the list.")]
        public string group_name { get; set; }

        [Display(Name = "Sub Gr Code")]
        [Required(ErrorMessage = "Sub group code is required.")]
        [StringLength(4, ErrorMessage = "Sub group code must not be more than 4 char")]
        public string sub_group_code { get; set; }

        [Display(Name = "Sub Gr Name")]
        [Required(ErrorMessage = "Please select Sub group name from the list.")]
        public string sub_group_name { get; set; }

        [Display(Name = "SSGC")]
        [Required(ErrorMessage = "Sub sub group code is required.")]
        [StringLength(6, ErrorMessage = "Sub sub group code must not be more than 6 char")]
        public string sub_sub_group_code { get; set; }

        [Display(Name = "SS Gr Desc")]
        [Required(ErrorMessage = "SS Group Description is required.")]
        [StringLength(50, ErrorMessage = "SS Group Description must not be more than 50 char")]
        public string long_desc { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
               
        public string last_edited_by { get; set; }        
        public DateTime last_edited_date { get; set; }

        public List<SubSubGroupMasterModel> SubSubGroupMasterList { get; set; }
    }
}