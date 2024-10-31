
using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Web;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class Report_PurchaseOrderController : Controller
    {
        // GET: Report_PurchaseOrder
        
        public ActionResult PO_Datewise()
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);
            mymodel.PurchaseHeaderSearch = Header;

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSM.SiteMasterList(), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");

            ClsPurchaseHeader lsPurchaseHeader = new ClsPurchaseHeader();
            mymodel.PurchaseHeader = lsPurchaseHeader.ReportPurchaseOrder_Datewise("purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult PO_Datewise(PurchaseOrderModel Model)
        {        
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.PurchaseHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.PurchaseHeaderSearch.ToDate);
            string lsFilter = string.Empty;

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSM.SiteMasterList(), "site_id", "site_name", Model.PurchaseHeaderSearch.SiteId);
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");

            if (Model.PurchaseHeaderSearch.SiteId != null)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + Model.PurchaseHeaderSearch.SiteId + " and ";
            }
            if (Model.PurchaseHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + "purchase_header.party_id = " + Model.PurchaseHeaderSearch.PartyId + " and ";
            }
            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            ClsPurchaseHeader lsPurchaseHeader = new ClsPurchaseHeader();
            mymodel.PurchaseHeader = lsPurchaseHeader.ReportPurchaseOrder_Datewise(lsFilter);
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult PO_Datewise_ExportToExcel(PurchaseOrderModel Model)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.PurchaseHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.PurchaseHeaderSearch.ToDate);
            string lsFilter = string.Empty;

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSM.SiteMasterList(), "site_id", "site_name", Model.PurchaseHeaderSearch.SiteId);
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");

            if (Model.PurchaseHeaderSearch.SiteId != null)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + Model.PurchaseHeaderSearch.SiteId + " and ";
            }
            if (Model.PurchaseHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + "purchase_header.party_id = " + Model.PurchaseHeaderSearch.PartyId + " and ";
            }
            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            ClsPurchaseHeader lsPurchaseHeader = new ClsPurchaseHeader();
            mymodel.PurchaseHeader = lsPurchaseHeader.ReportPurchaseOrder_Datewise(lsFilter);

            DataTable dt = lsPurchaseHeader.ReportPurchaseOrder_Datewise_ExportData(lsFilter);
            string FileName = "PO Datewise Report";
            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    dt.TableName = "TEST";
            //    wb.Worksheets.Add(dt);
            //    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //    wb.Style.Font.Bold = true;

            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("content-disposition", "attachment;filename= " + FileName + ".xlsx");

            //    using (MemoryStream MyMemoryStream = new MemoryStream())
            //    {
            //        wb.SaveAs(MyMemoryStream);
            //        MyMemoryStream.WriteTo(Response.OutputStream);
            //        Response.Flush();
            //        Response.End();
            //    }
            //}

            return View(mymodel);
        }

        //[HttpPost]
        //public ActionResult PO_Datewise_ExportToPdf(PurchaseOrderModel Model)
        //{
        //    string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.PurchaseHeaderSearch.FromDate);
        //    string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.PurchaseHeaderSearch.ToDate);
        //    string lsFilter = string.Empty;

        //    ClsSiteMaster lsSM = new ClsSiteMaster();
        //    ViewBag.SiteList = new SelectList(lsSM.SiteMasterList(), "site_id", "site_name", Model.PurchaseHeaderSearch.SiteId);
        //    ClsPartyMaster lsPM = new ClsPartyMaster();
        //    ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");

        //    if (Model.PurchaseHeaderSearch.SiteId != null)
        //    {
        //        lsFilter = lsFilter + "purchase_header.site_id = " + Model.PurchaseHeaderSearch.SiteId + " and ";
        //    }
        //    if (Model.PurchaseHeaderSearch.PartyId != null)
        //    {
        //        lsFilter = lsFilter + "purchase_header.party_id = " + Model.PurchaseHeaderSearch.PartyId + " and ";
        //    }
        //    lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

        //    PurchaseOrderModel mymodel = new PurchaseOrderModel();
        //    ClsPurchaseHeader lsPurchaseHeader = new ClsPurchaseHeader();
        //    mymodel.PurchaseHeader = lsPurchaseHeader.ReportPurchaseOrder_Datewise(lsFilter);

        //    DataTable dt = lsPurchaseHeader.ReportPurchaseOrder_Datewise_ExportData(lsFilter);
        //    string FileName = "PO Datewise Report";
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        Document document = new Document(PageSize.A2.Rotate());
        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //        document.Open();

        //        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
        //        document.Add(new Paragraph("PO Datewise Report", titleFont));
        //        document.Add(new Paragraph(" "));

        //        PdfPTable table = new PdfPTable(dt.Columns.Count);
        //        table.WidthPercentage = 100;

        //        float[] columnWidths = new float[] { 3f, 2f, 2f, 4f, 2f, 2.5f, 2f, 2f, 2f, 1.5f, 1.5f, 1.5f, 3f };

        //        table.SetWidths(columnWidths);

        //        BaseColor faintColor = new BaseColor(211, 211, 211);
        //        float faintBorderWidth = 0.5f;

        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK)))
        //            {
        //                HorizontalAlignment = Element.ALIGN_CENTER,
        //                Padding = 10,
        //                BackgroundColor = BaseColor.WHITE,
        //                BorderColor = faintColor,
        //                BorderWidth = faintBorderWidth
        //            };
        //            table.AddCell(cell);
        //        }

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            foreach (var cellData in row.ItemArray)
        //            {
        //                PdfPCell cell = new PdfPCell(new Phrase(cellData.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK)))
        //                {
        //                    HorizontalAlignment = Element.ALIGN_CENTER,
        //                    Padding = 10,
        //                    BorderColor = faintColor,
        //                    BorderWidth = faintBorderWidth
        //                };
        //                table.AddCell(cell);
        //            }
        //        }

        //        document.Add(table);
        //        document.Close();

        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();

        //        Response.Clear();
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");
        //        Response.Buffer = true;
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.BinaryWrite(bytes);
        //        Response.End();
        //    }
        //    return View(mymodel);
        //}

        public ActionResult Purchase_Order()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            int company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsPurchaseDetail PD = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();
            ClsStateMaster lsState = new ClsStateMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");
            ClsPurchaseHeader PH = new ClsPurchaseHeader();
            ViewBag.PurchaseOrderList = new SelectList(PH.PurchaseHeader(), "po_id", "po_no");
            mymodel.PurchaseHeader = PH.PurchaseHeader();
            mymodel.Company = lsCompany.CompanyMaster(company_id.ToString());
            mymodel.PurchaseHeaderSearch = PH.PurchaseHeader(1);
            mymodel.PurchaseDetail = PD.ModifyPurchaseDetail(1);
            mymodel.PurchaseTax = PT.PurchaseTaxes("purchase_taxes.po_id=" + 1 + " ");
            mymodel.PartyMasterSearch = lsPM.PartyMaster(mymodel.PurchaseHeaderSearch.party_id.ToString());
            mymodel.StateMst = lsState.StateMaster(mymodel.PartyMasterSearch.state_id);
            ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();
            mymodel.TermsCondition = TCM.TermsCondition("3");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Purchase_Order(PurchaseOrderModel mymodel)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            int company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            int PartyId= Convert.ToInt32(mymodel.PurchaseHeaderSearch.PartyId);
            int PoId = Convert.ToInt32(mymodel.PurchaseHeaderSearch.PoNo);
            PurchaseHeaderModel Header = new PurchaseHeaderModel();            
            ClsPurchaseHeader PH = new ClsPurchaseHeader();
            ClsPurchaseDetail PD = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            mymodel.PurchaseHeaderSearch = PH.PurchaseHeader(PoId);           
            mymodel.PurchaseDetail = PD.ModifyPurchaseDetail(PoId);
            mymodel.PurchaseTax = PT.PurchaseTaxes("purchase_taxes.po_id=" + PoId + " ");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");
            ViewBag.PurchaseOrderList = new SelectList(PH.PurchaseHeader(), "po_id", "po_no");
            mymodel.PartyMasterSearch = lsPM.PartyMaster(PartyId.ToString());
            mymodel.Company = lsCompany.CompanyMaster(company_id.ToString());
            mymodel.PurchaseHeader = PH.PurchaseHeader();          
            return View(mymodel);
        }

        public ActionResult Purchase_OrderView(int PoId, int PartyId)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFilter = string.Empty;

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            ClsPurchaseHeader PH = new ClsPurchaseHeader();
            ClsPurchaseDetail PD = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsStateMaster lsState = new ClsStateMaster();
            mymodel.PurchaseHeaderSearch = PH.PurchaseHeader(PoId);
            mymodel.PurchaseHeaderSearch.PartyId = Convert.ToInt32(PartyId);
            mymodel.PurchaseDetail = PD.ModifyPurchaseDetail(PoId);
            mymodel.PurchaseTax = PT.PurchaseTaxes("purchase_taxes.po_id=" + PoId + " ");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");
            ViewBag.PurchaseOrderList = new SelectList(PH.PurchaseHeader(), "po_id", "po_no");
            mymodel.PartyMasterSearch = lsPM.PartyMaster(PartyId.ToString());
            //mymodel.Company = lsCompany.CompanyMaster("1");
            mymodel.StateMst = lsState.StateMaster(mymodel.PartyMasterSearch.state_id);
            mymodel.Company = lsCompany.CompanyMaster(mymodel.PurchaseHeaderSearch.company_id.ToString());

            ClsSoftwareReportSetting lsSoftware = new ClsSoftwareReportSetting();
            mymodel.ReportSetting = lsSoftware.ReportModel(6);

            if (SiteId != 0)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + SiteId + " and ";
            }
            lsFilter = lsFilter + "purchase_header.po_id IS Not NULL";
            //lsFilter = lsFilter + "po_import = 'false' and purchase_header.po_close_flag = 'False' and purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            mymodel.PurchaseHeader = PH.PurchaseHeaderList(lsFilter); //PurchaseHeader();  
            ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();
            mymodel.TermsCondition = TCM.TermsCondition("3");
            return View("Purchase_Order",mymodel);
        }

        [HttpPost]
        public ActionResult Purchase_OrderView(PurchaseOrderModel mymodel)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFilter = string.Empty;
            string PoType = mymodel.PurchaseHeaderSearch.PoType;
            int PartyId = Convert.ToInt32(mymodel.PurchaseHeaderSearch.PartyId);
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            ClsPurchaseHeader PH = new ClsPurchaseHeader();
            ClsPurchaseDetail PD = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsStateMaster lsState = new ClsStateMaster();
            mymodel.PurchaseHeaderSearch = PH.PurchaseHeader(Convert.ToInt32(mymodel.PurchaseHeaderSearch.po_id));
            mymodel.PurchaseHeaderSearch.PartyId = Convert.ToInt32(PartyId);
            mymodel.PurchaseDetail = PD.ModifyPurchaseDetail(Convert.ToInt32(mymodel.PurchaseHeaderSearch.po_id));
            mymodel.PurchaseTax = PT.PurchaseTaxes("purchase_taxes.po_id=" + Convert.ToInt32(mymodel.PurchaseHeaderSearch.po_id) + " ");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");
            ViewBag.PurchaseOrderList = new SelectList(PH.PurchaseHeader(), "po_id", "po_no");
            mymodel.PartyMasterSearch = lsPM.PartyMaster(PartyId.ToString());
            mymodel.Company = lsCompany.CompanyMaster("1");
            mymodel.StateMst = lsState.StateMaster(mymodel.PartyMasterSearch.state_id);

            if (SiteId != 0)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + SiteId + " and ";
            }

            if (PoType != null )
            {
                lsFilter = lsFilter + "purchase_header.po_type = '" + PoType + "' and ";
            }
            lsFilter = lsFilter + "purchase_header.po_id IS Not NULL";
            //lsFilter = lsFilter + "po_import = 'false' and purchase_header.po_close_flag = 'False' and purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            mymodel.PurchaseHeader = PH.PurchaseHeaderList(lsFilter); //PurchaseHeader();           
            ClsTermsConditionMaster TCM = new ClsTermsConditionMaster();
            mymodel.TermsCondition = TCM.TermsCondition("3");
            return View("Purchase_Order", mymodel);
        }

        public JsonResult PartyWisePO(int PartyId)
        {
            ClsPurchaseHeader PH = new ClsPurchaseHeader();
            List<PurchaseHeaderModel> POList = new List<PurchaseHeaderModel>();
            POList = PH.PurchaseHeaderList("purchase_header.party_id= "+ PartyId + " ");
            return Json(new SelectList(POList, "po_id", "po_no"));
        }

        public ActionResult PoClosePendingReport()
        {
            ViewBag.Titlename = "Purchase Order Close/Pending";
           
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);
            mymodel.PurchaseHeaderSearch = Header;
            ClsIndentHeader lsIndentHeader = new ClsIndentHeader();
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();           
            mymodel.PurchaseHeader = lsPH.ReportPo_PendingClose("purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult PoClosePendingReport(PurchaseOrderModel POM)
        {
            string lsFilter = string.Empty;
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", POM.PurchaseHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", POM.PurchaseHeaderSearch.ToDate);
            ViewBag.Titlename = "Purchase Order Close/Pending";

            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";
            ClsIndentHeader lsIndentHeader = new ClsIndentHeader();
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            POM.PurchaseHeader = lsPH.ReportPo_PendingClose(lsFilter);
            return View(POM);
        }
    }
}