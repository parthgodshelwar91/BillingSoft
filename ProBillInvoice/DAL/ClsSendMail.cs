using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ProBillInvoice.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Web.Mail;
using MailMessage = System.Net.Mail.MailMessage;
using MailPriority = System.Net.Mail.MailPriority;

namespace ProBillInvoice.DAL
{
    public class ClsSendMail
    {
        private string _connString;
        string SqlQry;
        
        public ClsSendMail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }
                
        public string SendMail()
        {
            MailModel M = new MailModel();

            //M.From = "info@rotomitra.com";
            //// M.cc = "dahikar.rishi@gmail.com";
            //M.To = "info@rotomitra.com";
            //M.subject = "Test Mail";
            //M.mailbody = "Its Test Mails From Api";
            //M.username = "info@rotomitra.com";
            //M.password = "rot123ROT!@#";

            //----------------------------------------------
            M.From = "info@rmcsoft.in";
            // M.cc = "dahikar.rishi@gmail.com";
            M.To = "ankitavairagade013@gmail.com";
            M.subject = "GOOD BY";
            //M.mailbody = "GOOD BY";
            M.username = "info@rmcsoft.in";
            M.password = "inf123INF!@#";

            //using (var client = new HttpClient())
            //{

            //    client.BaseAddress = new Uri("https://lead.nspirehost.com/api/");

            //    //HTTP POST
            //    var postTask = client.PostAsJsonAsync<MailModel>("email", M);
            //    postTask.Wait();

            //    var result = postTask.Result;
            //}
            object returnValue = "";
            return returnValue.ToString();
        }
        public void sendInvoicemail(SaleInvoiceModel Model,string CertificateFile)
        {
            string htmlBody = $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Email</title>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                .container {{ padding: 20px; background-color: #f9f9f9; border: 1px solid #ddd; }}
                h1 {{ color: #333; }}
                p {{ font-size: 16px; }}
            </style>
        </head>
        <body>
            <div class='container'>
<div class='card h-100'>
<div class='qout-mailbox'>

                                              <div class='quot-title-main' style='background-color: #0f77d3; padding: 20px; text-align: center;'><h2  style='text - align: center; font-size: 22px; color: #fff;'>INVOICE - {Model.SaleInvoice.invoice_no}</h2></div>
											<div class='qout-inside-box' style='width: 50%; margin: 40px auto 0 auto;'>
												<div class='qout-contain-data' style='color: #666;'>
													<p>Dear {Model.SaleInvoice.party_name},</p>
													<p>Thank you for contacting us.Your Quotation can be viewed, printed and downloaded as PDF from the link below.</p>
												</div>
												<div class='qout-amount-value' style='background-color: #fdfdeb; border: #d5c79d solid 1px; padding: 50px;text-align: center;border-radius: 10px;'>
													<p class='text-center'><strong>INVOICE AMOUNT</strong></p>
													<h3 style='color: #e1541c; font-weight: 600;'><i class='las la-rupee-sign'></i>{Model.SaleInvoice.item_value}</h3>	
													<hr>
													<div style = 'margin: 0 0 30px 15%;'>
                                                        <p> INVOICE NO<strong style='padding: 0 10px; border-left: #ddd solid 1px; margin: 0 10px;'> {Model.SaleInvoice.invoice_no}</strong></p>
														<p>INVOICE Dt<strong style= 'padding: 0 10px; border-left: #ddd solid 1px; margin: 0 15px;' > {@String.Format("{0:dd/MM/yyyy}", Model.SaleInvoice.invoice_date)} </ strong ></ p >
                                                  </div >
                                                    <a href= '#' class='btn btn-primary me-3'>VIEW STATEMENT</a>
												</div>
												<div class='qout-regd-data' style ='color: #888;padding: 30px 0;'>
													<p style = 'color: #333;'> Regards,</p><br>
													<p>SIDHESHWAR RMC</p>
													<p>M: 9890095926, M: 9823190086</p>
												</div>
											</div>
										</div>     
            </div>             
 </div>
</div>  
        </body>
        </html>";
            
            MailModel M = new MailModel();
            
            //M.From = "info@rotomitra.com";
            //M.cc = "dahikar.rishi@gmail.com";
            //M.To = "info@rotomitra.com";
            //M.subject = "Test Mail";
            //M.mailbody = "Its Test Mails ";
            //M.username = "info@rotomitra.com";
            //M.password = "rot123ROT!@#";
            //-----------------------------------------
            M.From = "info@rmcsoft.in";
            M.cc = "dahikar.rishi@gmail.com";
            M.To = "ankitavairagade013@gmail.com";
            M.subject = "GOOD BY";
            M.mailbody = htmlBody;
            M.username = "info@rmcsoft.in";
            M.password = "inf123INF!@#";

            MailMessage MM = new MailMessage();

            MM.Body = M.mailbody;            
            MM.Attachments.Add(new Attachment(CertificateFile));

            MM.From = new MailAddress(M.From);
            MM.To.Add(M.To);
            MM.CC.Add(M.cc);
            MM.Subject = M.subject;
            MM.IsBodyHtml = true;
            MM.BodyEncoding = System.Text.Encoding.UTF8;
            MM.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();          
            client.Credentials = new System.Net.NetworkCredential(M.username, "inf123INF!@#");
            client.Port = Convert.ToInt32("25");
            client.Host = "server.nspirehost.net";

            client.EnableSsl = true;

            client.Send(MM);
        }

        

        







        //public string FindVocherNo(int voucher_id, string financial_year)
        //{
        //    string sqlQry = "SELECT ISNULL(voucher_no, '') AS voucher_no FROM voucher_entry ";
        //    sqlQry = sqlQry + "WHERE voucher_id = " + voucher_id + " AND financial_year = '" + financial_year + "' ";

        //    SqlConnection con = new SqlConnection(_connString);
        //    SqlCommand cmd = new SqlCommand(sqlQry, con);
        //    cmd.CommandType = CommandType.Text;

        //    object returnValue = "";
        //    using (con)
        //    {
        //        con.Open();
        //        returnValue = cmd.ExecuteScalar();
        //        con.Close();
        //    }
        //    return returnValue.ToString();
        //}

        //    Public Sub SendMail(ByVal EmailAddress As String, ByVal MailSubject As String, ByVal MailAttachment As String)
        //    Try
        //        Dim emailSettings As New MasterDataSetTableAdapters.software_email_settingTableAdapter
        //        emailSettings.Fill(pDataSet.software_email_setting)
        //        Dim SmtpServer As New Net.Mail.SmtpClient()
        //        Dim mail As New Net.Mail.MailMessage()
        //        SmtpServer.Credentials = New Net.NetworkCredential(pDataSet.software_email_setting.FindByemail_id(0).email_address, pDataSet.software_email_setting.FindByemail_id(0).email_password)
        //        SmtpServer.Port = 587
        //        SmtpServer.EnableSsl = True
        //        SmtpServer.Host = pDataSet.software_email_setting.FindByemail_id(0).host_name
        //        mail = New Net.Mail.MailMessage()
        //        mail.From = New Net.Mail.MailAddress(pDataSet.software_email_setting.FindByemail_id(0).email_address)
        //        If String.IsNullOrEmpty(EmailAddress) Then
        //            EmailAddress = pDataSet.software_email_setting.FindByemail_id(0).cc_email_address
        //        End If
        //        mail.To.Add(EmailAddress)
        //        mail.CC.Add(pDataSet.software_email_setting.FindByemail_id(0).cc_email_address)
        //        'mail.Bcc.Add("abhay@nspiretech.com")
        //        mail.Subject = MailSubject
        //        mail.Body = "Dear Sir/Madam" & vbCrLf & vbCrLf & " " & vbCrLf & vbCrLf & "Please find attachment." & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & "This is computer generated mail." & ""
        //        Dim attch As New Net.Mail.Attachment(MailAttachment)
        //        mail.Attachments.Add(attch)
        //        SmtpServer.Send(mail)
        //    Catch ex As Exception
        //        ErrorLogger.WriteToErrorLogger(ex.Message, ex.StackTrace, "EMAIL")
        //    End Try
        //End Sub
    }
}
