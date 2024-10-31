using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ProBillInvoice.Models
{
    public class MailModel
    {
        public string FileName { get; set; }    
    public byte[] PdfBytes { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string cc { get; set; }
        public string subject { get; set; }
        public string mailbody { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public MultipartFormDataContent formData { get; set; }
        public ByteArrayContent pdfContent { get; set; }
    }

    public class MailgunRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        //public string AttachmentName { get; set; }
       // public string AttachmentBase64 { get; set; }
        public string mailbody { get; set; }
        public List<string> Attachments { get; set; } = new List<string>(); 
        public string PdfFilePath { get; set; }
    }
}