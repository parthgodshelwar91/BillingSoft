using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Text;
namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class Report_InvoiceController : Controller
    {
        string lsFilter = string.Empty;
        // GET: Report_Invoice
        public ActionResult Index()
        {
            return View();
        }
               
        //***** SaleInvoiceReport **************************************
        public ActionResult SaleInvoiceReport()
        {
            Session["ViewType"] = "SaleInvoiceReport";
            SaleInvoiceModel mymodel = new SaleInvoiceModel();            
            ClsSaleInvoiceHeader lsheader = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail lsDetail = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();
            mymodel.Company = CM.CompanyMaster("1");
            mymodel.InvoiceHeaderSearch = lsheader.SaleInvoice(0);
            mymodel.InvoiceHeader = lsheader.SaleInvoiceHeader();
            mymodel.TermsCondition = TCM.TermsCondition("4");
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ViewBag.HeaderList = new SelectList(lsheader.SaleInvoiceHeader(), "sale_invoice_id", "invoice_no");
            return View(mymodel);
        }

        public ActionResult SaleInvoice_View(int InvoiceId, int PartyId)
        {
            int SiteId = 2;// Convert.ToInt32(Session["LoginSiteId"].ToString());

            SaleInvoiceModel mymodel = new SaleInvoiceModel();
            SaleInvoiceHeaderModel Header = new SaleInvoiceHeaderModel();
            ClsSaleInvoiceHeader IH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail ID = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsStateMaster lsState = new ClsStateMaster();
            ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();
            mymodel.InvoiceHeaderSearch = IH.SaleInvoice(InvoiceId);
            mymodel.InvoiceHeaderSearch.PartyId = Convert.ToInt32(PartyId);
            lsFilter = "sale_invoice_detail.sale_invoice_id  = " + InvoiceId + " and ";
            lsFilter = lsFilter + " sale_invoice_detail.sale_invoice_id IS Not NULL ";
            mymodel.InvoiceDetails = ID.InvoiceReport(lsFilter);
            mymodel.InvoiceTax = lsTax.InvoiceTax(InvoiceId);
            ClsPartyMaster lsPM = new ClsPartyMaster();          
            mymodel.partymaster = lsPM.PartyMaster(PartyId.ToString());
            mymodel.StateMst = lsState.StateMaster(mymodel.partymaster.state_id);
            mymodel.Company = lsCompany.CompanyMaster(mymodel.InvoiceHeaderSearch.company_id.ToString());
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ViewBag.HeaderList = new SelectList(IH.SaleInvoiceHeader(), "sale_invoice_id", "invoice_no");
            mymodel.TermsCondition = TCM.TermsCondition("4");
            string lsFilter1 = string.Empty;
            //if (SiteId != 0)
            //{
            //    lsFilter1 = lsFilter1 + "sale_invoice_header.site_id = " + SiteId + " AND ";
            //}
            lsFilter1 = lsFilter1 + "sale_invoice_header.sale_invoice_id IS Not NULL";

            mymodel.InvoiceHeader = IH.SaleInvoieHeaderList(lsFilter1); //SaleInvoiceHeader();
            return View("SaleInvoiceReport", mymodel);
        }


        //***** SaleInvoiceReport Consolidated **************************************
        public ActionResult SaleInvoiceReport_Consolidated(int InvoiceId, string PartyId)
        {
            string lsFilter = string.Empty;
            SaleInvoiceModel mymodel = new SaleInvoiceModel();

            lsFilter = "sale_invoice_detail.sale_invoice_id  = " + InvoiceId + " and ";
            lsFilter = lsFilter + "sale_invoice_detail.sale_invoice_id IS Not NULL ";
            ClsSaleInvoiceHeader IH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail lsDetail = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsStateMaster lsState = new ClsStateMaster();
            ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();
            mymodel.InvoiceHeaderSearch = IH.SaleInvoice(InvoiceId);
            mymodel.InvoiceHeaderSearch.PartyId = Convert.ToInt32(mymodel.InvoiceHeaderSearch.party_id);
            mymodel.InvoiceDetails = lsDetail.TaxInvoiceReport(lsFilter);
            mymodel.InvoiceTax = lsTax.InvoiceTax(InvoiceId);
            mymodel.Company = CM.CompanyMaster(mymodel.InvoiceHeaderSearch.company_id.ToString());
            mymodel.partymaster = lsPM.PartyMaster(mymodel.InvoiceHeaderSearch.party_id.ToString());
            mymodel.StateMst = lsState.StateMaster(mymodel.partymaster.state_id);
            mymodel.TermsCondition = TCM.TermsCondition("4");
            string lsFilter1 = string.Empty;
            //if (SiteId != 0)
            //{
            //    lsFilter1 = lsFilter1 + "sale_invoice_header.site_id = " + SiteId + " AND ";
            //}
            lsFilter1 = lsFilter1 + "sale_invoice_header.sale_invoice_id IS Not NULL";
            mymodel.InvoiceHeader = IH.SaleInvoieHeaderList(lsFilter1);
            return View(mymodel);
        }

        public ActionResult DeliveryReport(int InvoiceId, string PartyId)
        {
            string lsFilter = string.Empty;
            SaleInvoiceModel mymodel = new SaleInvoiceModel();

            lsFilter = "sale_invoice_detail.sale_invoice_id  = " + InvoiceId + " and ";
            lsFilter = lsFilter + "sale_invoice_detail.sale_invoice_id IS Not NULL ";
            ClsSaleInvoiceHeader IH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail lsDetail = new ClsSaleInvoiceDetail();
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsStateMaster lsState = new ClsStateMaster();
            mymodel.InvoiceHeaderSearch = IH.SaleInvoice(InvoiceId);
            mymodel.InvoiceDetails = lsDetail.DeliveryReport(lsFilter);
            mymodel.InvoiceHeaderSearch.PartyId = Convert.ToInt32(mymodel.InvoiceHeaderSearch.party_id);
            mymodel.Company = CM.CompanyMaster(mymodel.InvoiceHeaderSearch.company_id.ToString());
            mymodel.partymaster = lsPM.PartyMaster(mymodel.InvoiceHeaderSearch.party_id.ToString());
            mymodel.StateMst = lsState.StateMaster(mymodel.partymaster.state_id);
            
            string lsFilter1 = string.Empty;
            //if (SiteId != 0)
            //{
            //    lsFilter1 = lsFilter1 + "sale_invoice_header.site_id = " + SiteId + " AND ";
            //}
            lsFilter1 = lsFilter1 + "sale_invoice_header.sale_invoice_id IS Not NULL";
            mymodel.InvoiceHeader = IH.SaleInvoieHeaderList(lsFilter1);
            return View(mymodel);
        }


        public JsonResult SendmailStr(EmailDatastr emailDatas)
        {



            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(emailDatas.PDFBase64);
            

            MailModel M = new MailModel();

            M.From = "info@rotomitra.com";
            M.cc = emailDatas.Emailid;
            M.To = "info@rotomitra.com";
            M.subject = emailDatas.FileName ;
            M.mailbody = emailDatas.Emailbody;
            M.username = "info@rmcsoft.in";
            M.password = "inf123INF!@#";

            MailMessage MM = new MailMessage();
            Stream stream2 = new MemoryStream(bytes);
            MM.Attachments.Add(new Attachment(stream2, emailDatas.FileName + ".pdf"));
            MM.Body = M.mailbody;

            MM.From = new MailAddress(M.From);
            MM.To.Add(M.To);
            MM.CC.Add(M.cc);
            MM.Subject = M.subject;
            MM.IsBodyHtml = true;
            MM.BodyEncoding = System.Text.Encoding.UTF8;
            MM.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(M.username, M.password);
            client.Port = Convert.ToInt32("25");
            client.Host = "server.nspirehost.net";

            client.EnableSsl = true;

           // client.Send(MM);

        

            var response = new { success = true, responseText = "Your message successfuly sent!" };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}