using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using Newtonsoft.Json;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder 
        string lsViewType = string.Empty;
        public ActionResult Index()
        {
            //string lsFromDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, "04");
            string lsToDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", DateTime.Now);
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();            
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);
            Header.SiteId = SiteId;
            Header.company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            mymodel.PurchaseHeaderSearch = Header;

            string lsFilter = string.Empty;
            if(SiteId != 0)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + SiteId + " and ";
            }
                       
            lsFilter = lsFilter + "po_import = 'false' and purchase_header.po_close_flag = 'False' and purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";
            
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            mymodel.PurchaseHeader = lsPH.PurchaseHeaderList(lsFilter);

            ClsPurchaseDetail lPD = new ClsPurchaseDetail();
            List<PurchaseDetailModel> lsSD = new List<PurchaseDetailModel>();
            List<PurchaseDetailModel> lsSD2 = new List<PurchaseDetailModel>();
            foreach (var item in mymodel.PurchaseHeader)
            {                
                lsSD = lPD.ModifyPurchaseDetail(Convert.ToInt32(item.po_id));
                lsSD2.AddRange(lsSD);
            }
            mymodel.PurchaseDetail = lsSD2;

            return View(mymodel);          
        }

        [HttpPost]
        public ActionResult Search(PurchaseOrderModel PO)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            ViewBag.DepartmentList = new SelectList(lsDepartment.DepartmentMaster(), "dept_id", "dept_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            string lsFilter = string.Empty;
            if (PO.PurchaseHeaderSearch.SiteId != 0)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + PO.PurchaseHeaderSearch.SiteId + " and ";
            }
            if (PO.PurchaseHeaderSearch.company_id != null)
            {
                lsFilter = lsFilter + "purchase_header.company_id = " + PO.PurchaseHeaderSearch.company_id + " and ";
            }

            if (PO.PurchaseHeaderSearch.po_type != null)
            {
                lsFilter = lsFilter + "purchase_header.po_type = '"+ PO.PurchaseHeaderSearch.po_type + "' and ";
            }

            if (PO.PurchaseHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + "purchase_header.party_id = " + PO.PurchaseHeaderSearch.PartyId + " and ";
            }
                       
            if (PO.PurchaseHeaderSearch.approval_flag != false)
            {
                lsFilter = lsFilter + "purchase_header.approval_flag = '" + PO.PurchaseHeaderSearch.approval_flag + "' and ";
            }           

            if (PO.PurchaseHeaderSearch.po_close_flag == false)
            {
                lsFilter = lsFilter + "purchase_header.po_close_flag = '" + PO.PurchaseHeaderSearch.po_close_flag + "' and ";
            }
            else if (PO.PurchaseHeaderSearch.po_close_flag == true)
            {
                lsFilter = lsFilter + "purchase_header.po_close_flag = '" + PO.PurchaseHeaderSearch.po_close_flag + "' and ";
            }

            string lsFromDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", PO.PurchaseHeaderSearch.FromDate);
            string lsToDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", PO.PurchaseHeaderSearch.ToDate);
            lsFilter = lsFilter + "po_import = 'false' and purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ClsPurchaseDetail lsPD = new ClsPurchaseDetail();
            mymodel.PurchaseHeader = lsPH.PurchaseHeaderList(lsFilter);
            List<PurchaseDetailModel> lsSD = new List<PurchaseDetailModel>();
            List<PurchaseDetailModel> lsSD2 = new List<PurchaseDetailModel>();
            foreach (var item in mymodel.PurchaseHeader)
            {
                //SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = lsPD.ModifyPurchaseDetail(Convert.ToInt32(item.po_id));
                lsSD2.AddRange(lsSD);
            }
            mymodel.PurchaseDetail = lsSD2;
            return View("Index", mymodel);   
        }

        public ActionResult IndentPO()
        {
            ViewBag.Titlename = "IndentPO";
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            ClsPayterm lsPayterm = new ClsPayterm();
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            PurchaseDetailModel Detail = new PurchaseDetailModel();
            ClsPurchaseHeader lsHeader = new ClsPurchaseHeader();
            ClsPurchaseDetail lsDetail = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();

            Header.po_id = lsHeader.NextId();
            Header.company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsFinancialYear lsFinYear = new ClsFinancialYear();
            string CompanyCode = lsCompany.FindCompanyCode(Convert.ToInt32(Header.company_id));
            string FindSiteName = lsSM.FindSiteName(Convert.ToInt32(Header.company_id));
            string FinYear = lsFinYear.CurrentYear();
            Header.po_no = string.Format("{0}/{1}/{2}/{3}", "PO", CompanyCode, FinYear, lsHeader.NextNoCompanywise(Convert.ToInt32(Header.company_id), false, FinYear));
            Header.po_date = DateTime.Now;
            Header.del_completion_date = DateTime.Now.AddDays(10);
            Header.PurchaseHeaderList = lsHeader.PurchaseHeader();
            mymodel.PurchaseHeaderSearch = Header;
                        
            mymodel.PurchaseDetail = lsDetail.GetPurchaseDetail(SiteId);

            mymodel.PurchaseTax = PT.PurchaseTaxes("tax_id = 0 ");
            mymodel.PurchaseTax = new List<PurchaseTaxModel>();
            mymodel.PurchaseTax.Add(new PurchaseTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
           
            Session["ViewType"] = "IndentPO";
            return View(mymodel);
        }
   

        [HttpPost]
        public ActionResult IndentPO(PurchaseOrderModel POM, FormCollection collection, string btnAdd, string btndeleteTax)
        {
            ViewBag.Titlename = "IndentPO";
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
           
            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            ClsPurchaseHeader lPH = new ClsPurchaseHeader();
            ClsPurchaseDetail lPD = new ClsPurchaseDetail();
            ClsPurchaseTax lPT = new ClsPurchaseTax();

            switch (btndeleteTax)
            {
                case string number when int.TryParse(number, out var index):
                    if (POM.PurchaseTax.Count > 0)
                    {
                        POM.PurchaseTax.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.PurchaseDetail = POM.PurchaseDetail;
                    mymodel.PurchaseTax = POM.PurchaseTax;
                    mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                    break;
            }
            if (btnAdd == "AddTax")
            {
                List<PurchaseTaxModel> TaxTable = new List<PurchaseTaxModel>();
                foreach (var row in POM.PurchaseTax)
                {
                    PurchaseTaxModel tRow = new PurchaseTaxModel();
                    tRow.tax_id = row.tax_id;
                    tRow.po_id = row.po_id;
                    tRow.acct_id = row.acct_id;
                    tRow.basic_amount = row.basic_amount;
                    tRow.cgst = row.cgst;
                    tRow.sgst = row.sgst;
                    tRow.igst = row.igst;
                    tRow.tax_amount = row.tax_amount;
                    TaxTable.Add(tRow);
                }

                TaxTable.Add(new PurchaseTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
                mymodel.PurchaseTax = TaxTable;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = POM.PurchaseDetail;

            }
            else if (btnAdd == "Save")
            {
                List<PurchaseHeaderModel> lsPurchase = new List<PurchaseHeaderModel>();
                PurchaseHeaderModel header = POM.PurchaseHeaderSearch;
                header.Mode = 1;
                header.po_id = lPH.NextId();
                header.site_id = SiteId;
                header.financial_year = (string)Session["FinancialYear"];
                header.created_by = (string)Session["LoginUserName"];
                header.created_date = DateTime.Now;
                lPH.InsertUpdate(header);

                List<PurchaseDetailModel> detail = POM.PurchaseDetail;
                if (detail != null)
                {
                    foreach (var item in detail)
                    {
                        if (item.material_id != null)
                        {
                            if (item.is_select == true)
                            {
                                item.Mode = 1;
                                item.purchase_detail_id = 0;
                                item.po_id = header.po_id;
                                item.is_pending =true;
                                lPD.InsertUpdate(item);
                            }
                        }
                    }
                }
                ClsIndentHeader lsIH = new ClsIndentHeader();
                lsIH.IndentClose(Convert.ToInt32(header.po_id), Convert.ToInt32(header.site_id));

                List<PurchaseTaxModel> TaxTable = POM.PurchaseTax;
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (PurchaseTaxModel dRow in TaxTable)
                        {
                            dRow.po_id = header.po_id;
                           
                            if (dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 1;
                                    lPT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }

                ViewBag.Message = "Detail Save SuccessFully";
                //mymodel.PurchaseTax = POM.PurchaseTax;
                //mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                //mymodel.PurchaseDetail = POM.PurchaseDetail;
                mymodel = POM;
            }

            else
            {
                ModelState.Clear();
                int partyid =   Convert.ToInt32(POM.PurchaseHeaderSearch.party_id);
                string lsFilter = string.Empty;
                if (POM.PurchaseHeaderSearch.party_id != 0)
                {
                    lsFilter = lsFilter + " purchase_header.party_id = " + POM.PurchaseHeaderSearch.party_id + " AND ";
                }
                lsFilter = lsFilter + "(indent_detail.is_approved = 'True') AND(indent_detail.is_pending = 'True') AND(indent_header.site_id = " + SiteId + ")  ";
                ClsPurchaseDetail lsDetail = new ClsPurchaseDetail();

                POM.PurchaseDetail = lsDetail.IndentPurchaseDetail(lsFilter);
                mymodel.PurchaseTax = POM.PurchaseTax;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = POM.PurchaseDetail;
            }
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            ClsPayterm lsPayterm = new ClsPayterm();
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");
            return View(mymodel);
        }
     
        public ActionResult Edit(int Id)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Edit";
            PurchaseOrderModel Model = new PurchaseOrderModel();
            ClsPurchaseHeader PH = new ClsPurchaseHeader();
            ClsPurchaseDetail PD = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            Model.PurchaseHeaderSearch = PH.PurchaseHeader(Id);
            Model.PurchaseDetail = PD.ModifyPurchaseDetail1(Id);
            Model.PurchaseTax= PT.PurchaseTaxes("po_id= " + Id + " ");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsPayterm lsPayterm = new ClsPayterm();
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");
            Session["TaxTable"] = Model.PurchaseTax;
            return View(Model);
        }

        [HttpPost]
        public ActionResult Edit(PurchaseOrderModel POM, FormCollection form, string btnAdd)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Edit";
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsPayterm lsPayterm = new ClsPayterm();
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            ClsPurchaseHeader lPH = new ClsPurchaseHeader();
            ClsPurchaseDetail lPD = new ClsPurchaseDetail();
            ClsPurchaseTax lPT = new ClsPurchaseTax();            

            if (btnAdd == "AddTax")
            {
                List<PurchaseTaxModel> TaxTable = new List<PurchaseTaxModel>();
                if (POM.PurchaseTax != null)
                {
                    foreach (var row in POM.PurchaseTax)
                    {
                        PurchaseTaxModel tRow = new PurchaseTaxModel();
                        tRow.tax_id = row.tax_id;
                        tRow.po_id = row.po_id;
                        tRow.acct_id = row.acct_id;
                        tRow.basic_amount = row.basic_amount;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.tax_amount = row.tax_amount;
                        TaxTable.Add(tRow);
                    }
                }

                TaxTable.Add(new PurchaseTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
                mymodel.PurchaseTax = TaxTable;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = POM.PurchaseDetail;

            }
            else if (btnAdd == "Save")
            {
                PurchaseHeaderModel header = POM.PurchaseHeaderSearch;
                header.Mode = 2;                
                header.financial_year = (string)Session["FinancialYear"];
                header.last_edited_by = (string)Session["LoginUserName"];
                header.last_edited_date = DateTime.Now;
                lPH.InsertUpdate(header);
                List<PurchaseDetailModel> detail = POM.PurchaseDetail;
                if (detail != null)
                {
                    foreach (var item in detail)
                    {
                        ClsMaterialMaster lsmm = new ClsMaterialMaster();
                        MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + item.material_id + " ");
                        item.Mode = 2;                        
                        item.po_id = header.po_id;
                        item.alt_unit = MM.alt_unit;
                        item.unit_code = MM.unit_code;
                        item.con_factor = MM.con_factor;
                        lPD.InsertUpdate(item);
                    }
                }
                List<PurchaseTaxModel> TaxTable = POM.PurchaseTax;
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (PurchaseTaxModel dRow in TaxTable)
                        {
                            dRow.po_id = header.po_id;
                            if (dRow.tax_id > 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 2;
                                    lPT.InsertUpdate(dRow);
                                }
                            }
                            else if (dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 1;
                                    lPT.InsertUpdate(dRow);
                                }
                            }

                        }
                    }
                }
                ViewBag.Message = "Detail Save SuccessFully";
                mymodel.PurchaseTax = POM.PurchaseTax;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = POM.PurchaseDetail;
            }
            return View(mymodel);
        }

        public ActionResult AddPO()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "AddPO";

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            if (SiteId == 0)
            {
                ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("material_mst.material_type = 'Purchase' "), "material_id", "material_name");
            }
            else
            {
                ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + "AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");
            }            

            ClsUnitMaster lsUom = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");            
            ClsPayterm lsPayterm = new ClsPayterm();
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");            

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            PurchaseDetailModel Detail = new PurchaseDetailModel();
            ClsPurchaseHeader lsHeader = new ClsPurchaseHeader();
            ClsPurchaseDetail lsDetail = new ClsPurchaseDetail();
            ClsPurchaseTax PT = new ClsPurchaseTax();

            Header.po_id = lsHeader.NextId();
            Header.company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsFinancialYear lsFinYear = new ClsFinancialYear();
            string CompanyCode = lsCompany.FindCompanyCode(Convert.ToInt32(Header.company_id));
            string FindSiteName = lsSM.FindSiteName(Convert.ToInt32(Header.company_id));
            string FinYear = lsFinYear.CurrentYear();
            Header.po_no = string.Format("{0}/{1}/{2}/{3}", "PO", CompanyCode, FinYear, lsHeader.NextNoCompanywise(Convert.ToInt32(Header.company_id), false, FinYear));
            Header.po_date = DateTime.Now;
            Header.payterm_code = 0;
            Header.del_site_id = SiteId;
            Header.del_completion_date = DateTime.Now.AddDays(10);            
            Header.PurchaseHeaderList = lsHeader.PurchaseHeader();
            mymodel.PurchaseHeaderSearch = Header;

            mymodel.PurchaseDetail = new List<PurchaseDetailModel>();
            mymodel.PurchaseDetail.Add(new PurchaseDetailModel { purchase_detail_id = 0, ind_header_id = 0, po_id = 0, material_id = 0, brand_id = 0, unit_code = 0, stock_qty = Convert.ToDecimal("0.00"), item_qty = Convert.ToDecimal("0.00"), item_rate = Convert.ToDecimal("0.00"), discount = Convert.ToDecimal("0.00"), sub_total = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), item_value = Convert.ToDecimal("0.00"), total_rec_qty = 0, is_approved = false, is_pending = false, is_select = true, remarks = "" });

            mymodel.PurchaseTax = new List<PurchaseTaxModel>();
            mymodel.PurchaseTax.Add(new PurchaseTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });                                         
            Session["ViewType"] = "AddPO";
            return View(mymodel);            
        }

        [HttpPost]
        public ActionResult AddPO(PurchaseOrderModel POM,FormCollection collection, string btnSave, string btndeletePD,string btndeleteTax)
        {
            
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "AddPO";
            ClsPurchaseHeader lPH = new ClsPurchaseHeader();
            ClsPurchaseDetail lPD = new ClsPurchaseDetail();
            ClsPurchaseTax lPT = new ClsPurchaseTax();
            PurchaseOrderModel mymodel = new PurchaseOrderModel();

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            if (SiteId == 0)
            {
                ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("material_mst.material_type = 'Purchase' "), "material_id", "material_name");
            }
            else
            {
                ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + "AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");
            }            

            ClsUnitMaster lsUom = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsPayterm lsPayterm = new ClsPayterm();
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");

            switch (btndeletePD)
            {
                //case "AddLine":
                //    POM.PurchaseDetail.Add(new PurchaseDetailModel());
                //    break;

                //case "RemLine":
                //    if (POM.PurchaseDetail.Count > 0)
                //    {
                //        POM.PurchaseDetail.RemoveAt(POM.PurchaseDetail.Count - 1);
                //    }
                //    break;

                case string number when int.TryParse(number, out var index):
                    if (POM.PurchaseDetail.Count > 0)
                    {
                        POM.PurchaseDetail.RemoveAt(index);
                    }                    
                    ModelState.Clear();
                    mymodel.PurchaseDetail = POM.PurchaseDetail;
                    mymodel.PurchaseTax = POM.PurchaseTax;
                    mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                    break;
            }
            switch(btndeleteTax)
            {
                case string number when int.TryParse(number, out var index):
                    if (POM.PurchaseTax.Count > 0)
                    {
                        POM.PurchaseTax.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.PurchaseDetail = POM.PurchaseDetail;
                    mymodel.PurchaseTax = POM.PurchaseTax;
                    mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                    break;
            }
            
            if (btnSave == "Add")
            {
                List<PurchaseDetailModel> DetailTable = new List<PurchaseDetailModel>();
                if (POM.PurchaseDetail != null)
                {
                    foreach (var row in POM.PurchaseDetail)
                    {
                        PurchaseDetailModel tRow = new PurchaseDetailModel();
                        tRow.purchase_detail_id = row.purchase_detail_id;
                        tRow.ind_header_id = row.ind_header_id;
                        tRow.po_id = row.po_id;
                        tRow.material_id = row.material_id;
                        tRow.brand_id = row.brand_id;
                        tRow.unit_code = row.unit_code;
                        tRow.stock_qty = row.stock_qty;
                        tRow.item_qty = row.item_qty;
                        tRow.item_rate = row.item_rate;
                        tRow.discount = row.discount;
                        tRow.sub_total = row.sub_total;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.item_value = row.item_value;
                        tRow.total_rec_qty = row.total_rec_qty;
                        tRow.is_approved = row.is_approved;
                        tRow.is_pending = row.is_pending;
                        tRow.is_select = row.is_select;
                        tRow.remarks = row.remarks;
                        DetailTable.Add(tRow);
                    }
                }
                DetailTable.Add(new PurchaseDetailModel { purchase_detail_id = 0, ind_header_id = 0, po_id = 0, material_id = 0, brand_id = 0, unit_code = 0, stock_qty = Convert.ToDecimal("0.00"), item_qty = Convert.ToDecimal("0.00"), item_rate = Convert.ToDecimal("0.00"), discount = Convert.ToDecimal("0.00"), sub_total = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), item_value = Convert.ToDecimal("0.00"), total_rec_qty = 0, is_approved = false, is_pending = false, is_select = true, remarks = "" });

                mymodel.PurchaseTax = POM.PurchaseTax;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = DetailTable;
                //ViewBag.Remove = "N";
            }           

            else if (btnSave == "AddTax")
            {
                List<PurchaseTaxModel> TaxTable = new List<PurchaseTaxModel>();
                if (POM.PurchaseTax != null)
                {
                    foreach (var row in POM.PurchaseTax)
                    {
                        PurchaseTaxModel tRow = new PurchaseTaxModel();
                        tRow.tax_id = row.tax_id;
                        tRow.po_id = row.po_id;
                        tRow.acct_id = row.acct_id;
                        tRow.basic_amount = row.basic_amount;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.tax_amount = row.tax_amount;
                        TaxTable.Add(tRow);
                    }
                }

                TaxTable.Add(new PurchaseTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
                mymodel.PurchaseTax = TaxTable;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = POM.PurchaseDetail;

            }

            else if (btnSave == "Save")
            {
                PurchaseHeaderModel header = POM.PurchaseHeaderSearch;
                header.Mode = 1;
                header.po_id = lPH.NextId();
                header.site_id = SiteId;
                header.financial_year = (string)Session["FinancialYear"];
                header.created_by = (string)Session["LoginUserName"];
                header.created_date = DateTime.Now;
                lPH.InsertUpdate(header);

                List<PurchaseDetailModel> detail = POM.PurchaseDetail;
                
                if (detail != null)
                {
                    foreach (var item in detail)
                    {
                        if (item.material_id != null)
                        {
                            if (item.is_select == true)
                            {
                                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + item.material_id + " ");
                                item.Mode = 1;
                                item.purchase_detail_id = 0;
                                item.po_id = header.po_id;
                                item.is_pending = true;
                                item.alt_unit = MM.alt_unit;
                                item.unit_code = MM.unit_code;
                                item.con_factor = MM.con_factor;
                                lPD.InsertUpdate(item);
                                
                            }
                        }
                    }
                }
                List<PurchaseTaxModel> TaxTable = POM.PurchaseTax;
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (PurchaseTaxModel dRow in TaxTable)
                        {
                            dRow.po_id = header.po_id;
                           
                             if (dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 1;
                                    lPT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }
                ViewBag.Message = "Detail Save SuccessFully";
                mymodel.PurchaseTax = POM.PurchaseTax;
                mymodel.PurchaseHeaderSearch = POM.PurchaseHeaderSearch;
                mymodel.PurchaseDetail = POM.PurchaseDetail;
            }
            
            return View(mymodel);
        }

        
        public ActionResult PartyAddress(int id, string name)
        {
            ClsPartyMaster lsPM = new ClsPartyMaster();
            string lsAddress = lsPM.BillingAddress(id);

            try
            {
                return Json(new
                {
                    msg = lsAddress
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public ActionResult PartyDetail(int PartyId, string PartyName)
        {
            ClsPartyMaster lsPM = new ClsPartyMaster();           
            string PartyCodeAddress = lsPM.FillByPartyId(PartyId, "Supplier");

            try
            {
                return Json(new
                {
                    msg = PartyCodeAddress
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JsonResult FillByUnitCode(string ItemCode, string ItemName, string PartyId, int CompanyId)
        {
            if (string.IsNullOrEmpty(ItemCode))
            {
                ItemCode = "0";
            }
            ClsMaterialMaster lsmm = new ClsMaterialMaster();
            MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + ItemCode + " ");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            int PartyStateId = lsPM.FindStateId(Convert.ToInt32(PartyId));
            int CompanyStateId = lsCM.FindStateId(CompanyId);

            if (PartyStateId == CompanyStateId)
            {
                MM.igst = 0;
            }
            else
            {
                MM.cgst = 0;
                MM.sgst = 0;
            }


            ClsUnitMaster lsUom = new ClsUnitMaster();
            ClsBrandMaster lsBrand = new ClsBrandMaster();
            //var result = new { UomList = new SelectList(lsUom.GetUOMMaster(Convert.ToInt32(ItemCode)), "unit_code", "short_desc"), BrandList = new SelectList(lsBrand.GetBrandMaster(Convert.ToInt32(ItemCode)), "brand_id", "brand_name"),MM.cgst,MM.sgst,MM.igst };
            var result = new { UomList = new SelectList(lsUom.GetUOMMaster(Convert.ToInt32(MM.alt_unit)), "unit_code", "short_desc"), BrandList = new SelectList(lsBrand.GetBrandMaster(Convert.ToInt32(ItemCode)), "brand_id", "brand_name"), MM.cgst, MM.sgst, MM.igst };
            return Json(result);
        }

        public ActionResult PaytermDays(string PaytermCode, string PaytermName)
        {
            string PaytermDays = "0";
            if (PaytermCode != null && !string.IsNullOrEmpty(PaytermCode))
            {
                ClsPayterm lsPT = new ClsPayterm();
                PaytermDays = lsPT.PaytermDays(Convert.ToInt32(PaytermCode)).ToString();
            }
            try
            {
                return Json(new
                {
                    msg = PaytermDays
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult PONO(int Company_id, string Company_name)
        {            
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            ClsPurchaseHeader lsHeader = new ClsPurchaseHeader();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsFinancialYear lsFinYear = new ClsFinancialYear();
            string CompanyCode = lsCompany.FindCompanyCode(Company_id);            
            string FinYear = lsFinYear.CurrentYear();
            Header.po_no = string.Format("{0}/{1}/{2}/{3}", "PO", CompanyCode, FinYear, lsHeader.NextNoCompanywise(Company_id, false, FinYear));

            try
            {
                return Json(new
                {
                    msg = Header.po_no
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Delete(string id,string Unitcode)
        {
            return View();
        }

        public ActionResult Clone(int Id)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Clone";

            //ClsUnitMaster lsUom = new ClsUnitMaster();
            //ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");
            ClsPayterm lsPayterm = new ClsPayterm();
            ViewBag.PaytermList = new SelectList(lsPayterm.Payterm(), "payterm_code", "payterm_desc");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");

            PurchaseOrderModel Model = new PurchaseOrderModel();
            ClsPurchaseHeader lsHeader = new ClsPurchaseHeader();
            ClsPurchaseDetail lsDetail = new ClsPurchaseDetail();            
            ClsPurchaseTax lsTax = new ClsPurchaseTax();            
            Model.PurchaseHeaderSearch = lsHeader.PurchaseHeaderClone(Id);
            Model.PurchaseDetail = lsDetail.ModifyPurchaseDetail(Id);
            Model.PurchaseTax = lsTax.PurchaseTaxes("po_id = " + Id + " ");

            Model.PurchaseHeaderSearch.po_id = lsHeader.NextId();
            Model.PurchaseHeaderSearch.company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsFinancialYear lsFinYear = new ClsFinancialYear();
            string CompanyCode = lsCompany.FindCompanyCode(Convert.ToInt32(Model.PurchaseHeaderSearch.company_id));
            string FindSiteName = lsSM.FindSiteName(Convert.ToInt32(Model.PurchaseHeaderSearch.company_id));
            string FinYear = lsFinYear.CurrentYear();
            Model.PurchaseHeaderSearch.po_no = string.Format("{0}/{1}/{2}/{3}", "PO", CompanyCode, FinYear, lsHeader.NextNoCompanywise(Convert.ToInt32(Model.PurchaseHeaderSearch.company_id), false, FinYear));
            Model.PurchaseHeaderSearch.po_date = DateTime.Now;
            Model.PurchaseHeaderSearch.del_completion_date = DateTime.Now.AddDays(10);
            Model.PurchaseHeaderSearch.PurchaseHeaderList = lsHeader.PurchaseHeader();

            Session["ViewType"] = "AddPO";
            return View(Model);
        }

    }
}