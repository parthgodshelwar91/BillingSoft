//using Aspose.Pdf;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    public class EmailController : Controller
    {
        string lsFilter = string.Empty;
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }
        //------------------------Sale Invoice Mail----------------------------------------------
        public ActionResult SaleInvoiceEmail(int Id,int PartyId)
        {
            DataTable dt = new DataTable();
            SaleInvoiceModel Model = new SaleInvoiceModel();
            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsCompanyMaster CM = new ClsCompanyMaster();
            Model.InvoiceHeaderSearch = SIH.SaleInvoieHeader(Id);
            Model.Company = CM.CompanyMaster(Model.InvoiceHeaderSearch.company_id.ToString());

            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");

            Model.SaleInvoice = SID.SaleInvoiceReport(Id);
            Model.SaleInvoice.party_id = PartyId;         
            string cardFileName = GenerateInvoicePdf(Model);
            
            ClsSendMail lsmail = new ClsSendMail();
            lsmail.sendInvoicemail(Model, cardFileName);          
            return View("SaleInvoiceReport", Model);

        }
        public ActionResult CardPrint(SaleInvoiceDetailModel model)
        {
            SaleInvoiceModel mymodel = new SaleInvoiceModel();
            ClsSaleInvoiceDetail lsDetail = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            ClsCompanyMaster CM = new ClsCompanyMaster();
            mymodel.SaleInvoice = lsDetail.SaleInvoiceReport(model.sale_invoice_id);
            return View(mymodel);
        }
        public ActionResult SaleInvoiceReport(SaleInvoiceDetailModel model)
        {
            SaleInvoiceModel mymodel = new SaleInvoiceModel();
            //ClsSaleInvoiceHeader IH = new ClsSaleInvoiceHeader();
            //ClsSaleInvoiceDetail ID = new ClsSaleInvoiceDetail();
            //ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            //ClsCompanyMaster CM = new ClsCompanyMaster();
            //ClsStateMaster lsState = new ClsStateMaster();
            //ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();            
            //mymodel.InvoiceHeaderSearch = IH.SaleInvoice(model.sale_invoice_id);
            //mymodel.InvoiceHeaderSearch.PartyId = Convert.ToInt32(model.party_id);
            //lsFilter = "sale_invoice_detail.sale_invoice_id  = " + model.sale_invoice_id + " and ";
            //lsFilter = lsFilter + " sale_invoice_detail.sale_invoice_id IS Not NULL ";
            //mymodel.InvoiceDetails = ID.InvoiceReport(lsFilter);
            //mymodel.InvoiceTax = lsTax.InvoiceTax(model.sale_invoice_id);
            //ClsPartyMaster lsPM = new ClsPartyMaster();
            //mymodel.partymaster = lsPM.PartyMaster(model.party_id.ToString());
            //mymodel.StateMst = lsState.StateMaster(mymodel.partymaster.state_id);
            //mymodel.Company = CM.CompanyMaster(mymodel.InvoiceHeaderSearch.company_id.ToString());
            //ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            //ViewBag.HeaderList = new SelectList(IH.SaleInvoiceHeader(), "sale_invoice_id", "invoice_no");
            //mymodel.TermsCondition = TCM.TermsCondition("4");
            //string lsFilter1 = string.Empty;
            
            //lsFilter1 = lsFilter1 + "sale_invoice_header.sale_invoice_id IS Not NULL";
            //mymodel.InvoiceHeader = IH.SaleInvoieHeaderList(lsFilter1);
            return View(mymodel);
        }
        
        public string GenerateInvoicePdf(SaleInvoiceModel model)
        {
            //string domainName = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
            string scheme = HttpContext.Request.Url.Scheme;
            string URL = @Url.Action("SaleInvoiceReport", "Email", new { model.SaleInvoice.sale_invoice_id,model.SaleInvoice.party_id }, Request.Url.Scheme);
            //Create request for given url
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            //Create response-object
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Take response stream
            StreamReader sr = new StreamReader(response.GetResponseStream());
            //Read response stream (html code)
            string htmlString = sr.ReadToEnd();
            //Close streamreader and response
            sr.Close();
            response.Close();
            //PocketICard Size Height = 3.375 inches  Width = 2.125 inches

            MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlString));
            //HtmlLoadOptions options = new HtmlLoadOptions(URL);
            //options.PageInfo.IsLandscape = false;
            //options.PageInfo.Width = 306;   // in points 72pt = 1 inch
            //options.PageInfo.Height = 243;  // in points 72pt = 1 inch
            //options.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);
            //------------------------------------------------------------
            //options.PageInfo.Width = PageSize.A4.Height;
            //options.PageInfo.Height = PageSize.A4.Width;
            //options.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);
            string saveLocation = Server.MapPath("~/Doc_Files/");
            string filename = "Invoice_" + model.SaleInvoice.sale_invoice_id + ".pdf";
            //string filename = "Invoice_demo.pdf";
            string filepath = saveLocation + filename;
           
            // Load HTML file
            //Document pdfDocument = new Document(stream, options);
          //  Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(stream, options);
            //pdfDocument.Pages[1].Rotate = Rotation.on180;
          //  pdfDocument.Save(filepath);
            // }
            return filepath;
        }
        
        //------------------------Purchase Order Mail----------------------------------------------
        public ActionResult POEmail()
        {
            return View();
        }
    }
}