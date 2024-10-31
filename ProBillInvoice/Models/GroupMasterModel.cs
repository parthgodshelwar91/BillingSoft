using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class GroupMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
               
        [Display(Name = "Group Code")]
        [Required(ErrorMessage = "Group code is required.")]
        [StringLength(2, ErrorMessage = "Group Code must not be more than 2 char")]
        public string group_code { get; set; }

        [Display(Name = "Short Name")]
        [Required(ErrorMessage = "Group short name is required.")]
        [StringLength(2, ErrorMessage = "Group code must not be more than 2 char")]
        public string short_desc { get; set; }

        [Display(Name = "Group Desc")]
        [Required(ErrorMessage = "Group description is required.")]
        [StringLength(50, ErrorMessage = "Group description must not be more than 50 char")]
        public string long_desc { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
               
        public string last_edited_by { get; set; }        
        public DateTime last_edited_date { get; set; }

        public List<GroupMasterModel> GroupMasterList { get; set; }
    }
}