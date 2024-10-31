using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class EmailDatastr
    {
        public string PDFBase64 { get; set; }
        public string FileName { get; set; }
        public string Emailid { get; set; }
 
        public string Emailbody { get; set; }
    }
}