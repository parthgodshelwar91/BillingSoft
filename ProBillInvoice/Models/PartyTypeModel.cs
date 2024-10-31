using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class PartyTypeModel
    {        
        [Display(Name = "Type ID")]
        [Required(ErrorMessage = "Type ID is required.")]
        public int type_id { get; set; }

        [Display(Name = "Type Name")]
        [Required(ErrorMessage = "Type Name is required.")]
        public string type_name { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
        [Display(Name = "Category")]
        public string type_for { get; set; }

        public List<PartyTypeModel> PartyTypeList { get; set; }

    }
}