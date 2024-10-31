using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class PartyMstCustLocationModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        public int id { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer Name is required.")]
        public int party_id { get; set; }

        [Display(Name = "Customer Name")]
        public string party_name { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "City Name is required.")]
        public int location_id { get; set; }

        [Display(Name = "City Name")]
        public string city_name { get; set; }

        [Display(Name = "Site location")]
        public string location_detail { get; set; }

        [Display(Name = "Defunct")]
        public bool defunct { get; set; }

        public List<PartyMstCustLocationModel> CustomerPartyList { get; set; }
    }
}