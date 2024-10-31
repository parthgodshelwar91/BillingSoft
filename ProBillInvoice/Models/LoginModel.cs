using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class LoginModel
    {
        [Display(Name = "Email Or Username")] /*Username Or Email id*/
        [Required(ErrorMessage = "Please enter Username.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Store")]
        [Required(ErrorMessage = "Please Select Store.")]
        public int store_id { get; set; }

        [Display(Name = "Fy Year")]
        [Required(ErrorMessage = "Please Select Fy Year.")]
        public string Fyear { get; set; }
    }
}