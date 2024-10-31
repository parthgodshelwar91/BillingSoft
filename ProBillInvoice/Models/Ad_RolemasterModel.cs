using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    //private class Ad_RolemasterModel
    //{
    //    [Display(Name = "Sr No")]
    //    public int sr_no { get; set; }
    //    [Display(Name = "ROLE ID")]
    //    public int ROLE_ID { get; set; }
    //    [Display(Name = "ROLE NAME")]
    //    public string ROLE_NAME { get; set; }
    //}

    public class AspRoleMasterModel
    {
        [Display(Name = "ROLE ID")]
        public string ROLE_ID { get; set; }

        [Display(Name = "Role Name")]
        public string ROLE_NAME { get; set; }

        public List<AspRoleMasterModel> AspRoleMasterList { get; set; }

        public virtual ICollection<AspNetNavigationMenu> MenuList { get; set; }
        //public List<AspNetNavigationMenu> MenuList { get; set; }

    }

}