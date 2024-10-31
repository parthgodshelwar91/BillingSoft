using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class DepartmentMasterModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Dept ID")]
        [Required(ErrorMessage = "Dept ID is required.")]
        public int dept_id { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Dept is required.")]
        public string dept_name { get; set; }

        [Display(Name = "Defunct")]
        public bool defunct { get; set; }
        public List<DepartmentMasterModel> DepartmentList { get; set; }
    }
}