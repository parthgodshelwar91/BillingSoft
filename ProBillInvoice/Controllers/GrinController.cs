using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class GrinController : Controller
    {
        // GET: GrinHeader  
        public ActionResult Index()
        {
            //string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, "04");
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now);
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");            
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");         

            GrinModel mymodel = new GrinModel();
            GrinHeaderModel Header = new GrinHeaderModel();
            Header.SiteId = SiteId;
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);
            mymodel.GrinHeaderSearch = Header;

            string lsFilter = string.Empty;
            if (SiteId != 0)
            {
                lsFilter = lsFilter + "grin_header.site_id = " + SiteId + " and ";
            }
            lsFilter = lsFilter + "grin_header.grin_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            ClsGrinHeader lGH = new ClsGrinHeader();
            mymodel.GrinHeader = lGH.GrinHeaderList(lsFilter);

            ClsGrinDetails lGD = new ClsGrinDetails();
            List<GrinDetailModel> lsSD = new List<GrinDetailModel>();
            List<GrinDetailModel> lsSD2 = new List<GrinDetailModel>();
            foreach (var item in mymodel.GrinHeader)
            {
                //SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = lGD.GetGrinDetails("grin_header_id = "+ item .grin_header_id + " ");
                lsSD2.AddRange(lsSD);
            }
            mymodel.GrinDetail = lsSD2;

            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Search(GrinModel GRN)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");           

            string lsFilter = string.Empty;
            if (GRN.GrinHeaderSearch.SiteId != 0)
            {
                lsFilter = lsFilter + "grin_header.site_id = " + GRN.GrinHeaderSearch.SiteId + " and ";
            }
            if (GRN.GrinHeaderSearch.grin_type != null)
            {                
              lsFilter = lsFilter + "grin_header.grin_type = '"+ GRN.GrinHeaderSearch.grin_type + "' And ";     
            }
            if (GRN.GrinHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + "grin_header.party_id = " + GRN.GrinHeaderSearch.PartyId + " and ";
            }

            string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", GRN.GrinHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", GRN.GrinHeaderSearch.ToDate);
            lsFilter = lsFilter + "grin_header.grin_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            GrinModel mymodel = new GrinModel();
            ClsGrinHeader lGH = new ClsGrinHeader();
            mymodel.GrinHeader = lGH.GrinHeaderList(lsFilter);

            ClsGrinDetails lGD = new ClsGrinDetails();
            List<GrinDetailModel> lsSD = new List<GrinDetailModel>();
            List<GrinDetailModel> lsSD2 = new List<GrinDetailModel>();
            foreach (var item in mymodel.GrinHeader)
            {
                //SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = lGD.GetGrinDetails("grin_header_id = " + item.grin_header_id + " ");
                lsSD2.AddRange(lsSD);
            }
            mymodel.GrinDetail = lsSD2;

            return View("Index", mymodel);
        }

        public ActionResult GRN_WithPo()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string psFinancialYear = (string)Session["FinancialYear"];

            ClsSiteMaster lsSM = new ClsSiteMaster();
            string psSiteName = lsSM.FindSiteName(SiteId);
            int company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");            
            string CompanyCode = lsCompany.FindCompanyCode(company_id);                       

            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");
            ClsPartyMaster lsPM = new ClsPartyMaster();            
            ViewBag.PartyList = new SelectList(lsPM.FillByPendingPoParty("purchase_header.site_id = " + SiteId + " and po_import = 'false' and purchase_header.po_close_flag = 'False' AND purchase_header.approval_flag = 'True' "), "party_id", "party_name");                    
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeader(), "po_id", "po_no");

            GrinModel mymodel = new GrinModel();
            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();

            GrinHeaderModel header = new GrinHeaderModel();   
            header.grin_header_id = lGH.NextId();            
            header.grin_no = string.Format("{0}/{1}/{2}/{3}", "GRN", CompanyCode, psFinancialYear, lGH.NextNoCompanywise(company_id, psFinancialYear));
            header.grin_date = DateTime.Now;
            header.inv_date = DateTime.Now;
            header.gate_date = DateTime.Now;
            header.company_id = company_id;           
            mymodel.GrinHeaderSearch = header;
            mymodel.GrinTax = new List<GrinTaxModel>();
            mymodel.GrinTax.Add(new GrinTaxModel { tax_id = 0, acct_id = 0,  basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult GRN_WithPo(GrinModel Model, FormCollection form, string btnAdd,string btndeleteTax)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string psFinancialYear = (string)Session["FinancialYear"];

            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();
            GrinModel mymodel = new GrinModel();

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();            
            ViewBag.PartyList = new SelectList(lsPM.FillByPendingPoParty("purchase_header.site_id = " + SiteId + " and po_import = 'false' and purchase_header.po_close_flag = 'False' AND purchase_header.approval_flag = 'True' "), "party_id", "party_name");
            
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeaderList("purchase_header.party_id=" + Convert.ToInt32(Model.GrinHeaderSearch.party_id) + " and purchase_header.site_id = " + SiteId + " AND purchase_header.approval_flag = 'True' AND purchase_header.po_close_flag = 'False' "), "po_id", "po_no");
            
            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");
            ClsVoucherEntry lsvoucher = new ClsVoucherEntry();
            switch (btndeleteTax)
            {
                case string number when int.TryParse(number, out var index):
                    if (Model.GrinTax.Count > 0)
                    {
                        Model.GrinTax.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.GrinTax = Model.GrinTax;
                    mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                    mymodel.GrinDetail = Model.GrinDetail;
                    break;
            }

            if (btnAdd == "Add")
            {               
                GrinHeaderModel Header = Model.GrinHeaderSearch;               
                List<GrinTaxModel> TaxTable = new List<GrinTaxModel>();
                if (Model.GrinTax != null)
                {
                    foreach (var row in Model.GrinTax)
                    {
                        GrinTaxModel tRow = new GrinTaxModel();
                        tRow.tax_id = row.tax_id;
                        tRow.acct_id = row.acct_id;
                        tRow.basic_amount = row.basic_amount;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.tax_amount = row.tax_amount;
                        TaxTable.Add(tRow);
                    }
                }
                TaxTable.Add(new GrinTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
                
                mymodel.GrinTax = TaxTable;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;
            }

            else if (btnAdd == "Save")
            {
                List<GrinTaxModel> TaxTable = Model.GrinTax;
                GrinHeaderModel header = Model.GrinHeaderSearch;
                header.Mode = 1;
                header.grin_header_id = lGH.NextId();
                header.grin_type = "WITH PO";
                header.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());             
                header.financial_year = (string)Session["FinancialYear"];
                header.created_by = (string)Session["LoginUserName"];
                header.created_date = DateTime.Now;
                header.total_rec_amount = 0;
                header.grin_flag = false;
                lGH.InsertUpdate(header);

                List<GrinDetailModel> detail = Model.GrinDetail;
                if (detail != null)
                {
                    foreach (var item in detail)
                    {
                        if (item.is_select == true)
                        {
                            ClsMaterialMaster lsmm = new ClsMaterialMaster();
                            MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + item.material_id + " ");
                            item.Mode = 1;
                            item.grin_detail_id = 0;
                            item.grin_header_id = header.grin_header_id;
                            item.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());
                            item.alt_unit = MM.alt_unit;
                            item.unit_code = MM.unit_code;
                            item.con_factor = MM.con_factor;
                            lGD.InsertUpdate(item);
                        }
                    }
                }
                //---------------------Add Voucher Sale Purchase--------------------------
                int liIndex = 3;
                double ldTaxAmt = 0;
                int liAcctId = lsPM.FindPartyid1(header.party_id);
                lsvoucher.AddSalePurchaseVouchers(1, header.grin_no, 1, Convert.ToDateTime(header.grin_date), "Purchase", "PURC", "GR", "C", liAcctId, 18, Math.Round(header.total_amount, 0), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, 2, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "GR", "D", liAcctId, 18, header.basic_amount, header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);

                foreach (GrinTaxModel dRow in TaxTable) // Tax entry
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "D", liAcctId, Convert.ToInt32(dRow.acct_id), Convert.ToDecimal(dRow.tax_amount), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                    ldTaxAmt += Convert.ToDouble(dRow.tax_amount);
                    liIndex++;
                }
                double liRoundOff = lsvoucher.GetRoundOffValue(Convert.ToDouble(header.basic_amount) + ldTaxAmt);
                if (liRoundOff < 0)
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "C", liAcctId, 122, Convert.ToDecimal(liRoundOff) * -1, header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                }
                else if (liRoundOff > 0)
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "D", liAcctId, 121, Convert.ToDecimal(liRoundOff), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                }
                else
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "D", liAcctId, 121, Convert.ToDecimal(liRoundOff), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                }

                lsPH.PurchaseClose(Convert.ToInt32(header.po_id), Convert.ToInt32(header.site_id));
                lsPH.StockDetail_Purchase(Convert.ToInt32(header.grin_header_id), Convert.ToInt32(header.site_id));
               
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (GrinTaxModel dRow in TaxTable)
                        {
                            dRow.grin_header_id = header.grin_header_id;
                            if (dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 1;
                                    lsGT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }
                ViewBag.Message = "Detail Save SuccessFully";
                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;
            }
            else
            {
                ModelState.Clear();
                int po_id = Convert.ToInt32(Model.GrinHeaderSearch.po_id);
                mymodel.GrinDetail = lGD.FillByWithPO1(po_id);
                ClsPurchaseHeader lsPheader = new ClsPurchaseHeader();
                PurchaseHeaderModel PurchaseModel=lsPheader.PurchaseHeader(Convert.ToInt32(Model.GrinHeaderSearch.po_id));
                Model.GrinHeaderSearch.po_date = PurchaseModel.po_date;
                Model.GrinHeaderSearch.company_id = Convert.ToInt32(PurchaseModel.company_id);

                ClsFinancialYear lsFinYear = new ClsFinancialYear();
                //string psFinancialYear = lsFinYear.CurrentYear();
                string CompanyCode = lsCompany.FindCompanyCode(Model.GrinHeaderSearch.company_id);
                Model.GrinHeaderSearch.grin_no = string.Format("{0}/{1}/{2}/{3}", "GRN", CompanyCode, psFinancialYear, lGH.NextNoCompanywise(Model.GrinHeaderSearch.company_id, psFinancialYear));
                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;               
            }
            
            return View(mymodel);
        }

        public ActionResult GRN_WithOutPo()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string psFinancialYear = (string)Session["FinancialYear"];

            ClsSiteMaster lsSM = new ClsSiteMaster();
            string psSiteName = lsSM.FindSiteName(SiteId);
            int company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");
            string CompanyCode = lsCompany.FindCompanyCode(company_id);
            
            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + "AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");
            ClsUnitMaster lsUom = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");
                        
            GrinModel mymodel = new GrinModel();
            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();

            GrinHeaderModel header = new GrinHeaderModel();
            header.grin_header_id = lGH.NextId();            
            header.grin_no = string.Format("{0}/{1}/{2}/{3}", "GRN", CompanyCode, psFinancialYear, lGH.NextNoCompanywise(company_id, psFinancialYear)); 
            header.grin_date = DateTime.Now;
            header.inv_date = DateTime.Now;
            header.gate_date = DateTime.Now;
            header.company_id = company_id;
            mymodel.GrinHeaderSearch = header;
            
            mymodel.GrinDetail = new List<GrinDetailModel>();
            mymodel.GrinDetail.Add(new GrinDetailModel { grin_detail_id = 0, grin_header_id = 0, material_id = 0, unit_code = 0, mfg_date = "", po_qty = Convert.ToDecimal("0.00"), pend_qty= Convert.ToDecimal("0.00"), rece_qty = Convert.ToDecimal("0.00"), acce_qty = Convert.ToDecimal("0.00"), rej_qty = Convert.ToDecimal("0.00"), item_rate = Convert.ToDecimal("0.00"), sub_total = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), item_value = Convert.ToDecimal("0.00"), remarks = "" });

            mymodel.GrinTax = new List<GrinTaxModel>();
            mymodel.GrinTax.Add(new GrinTaxModel { tax_id = 0, grin_header_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
            return View(mymodel);
        }

        [HttpPost]            
        public ActionResult GRN_WithOutPo(GrinModel Model, FormCollection form, string btnAdd,string  btndeleteTax, string btndeleteGD)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + "AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");

            ClsUnitMaster lsUom = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");           

            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeaderList("purchase_header.party_id=" + Convert.ToInt32(Model.GrinHeaderSearch.party_id) + " and purchase_header.site_id = " + SiteId + " and purchase_header.po_close_flag = 'False' "), "po_id", "po_no");

            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();
            GrinModel mymodel = new GrinModel();

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");
            ClsVoucherEntry lsvoucher = new ClsVoucherEntry();
            switch (btndeleteGD)
            {
                case string number when int.TryParse(number, out var index):
                    if (Model.GrinDetail.Count > 0)
                    {
                        Model.GrinDetail.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.GrinTax = Model.GrinTax;
                    mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                    mymodel.GrinDetail = Model.GrinDetail;
                    break;
            }
            switch (btndeleteTax)
            {
                case string number when int.TryParse(number, out var index):
                    if (Model.GrinTax.Count > 0)
                    {
                        Model.GrinTax.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.GrinTax = Model.GrinTax;
                    mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                    mymodel.GrinDetail = Model.GrinDetail;
                    break;
            }

            if (btnAdd == "AddOne")
            {              
                List<GrinDetailModel> DetailTable = new List<GrinDetailModel>();
                if (Model.GrinDetail != null)
                {
                    foreach (var row in Model.GrinDetail)
                    {
                        GrinDetailModel tRow = new GrinDetailModel();
                        tRow.grin_detail_id = row.grin_detail_id;
                        tRow.grin_header_id = row.grin_header_id;
                        tRow.material_id = row.material_id;
                        tRow.unit_code = row.unit_code;
                        tRow.mfg_date = row.mfg_date;
                        tRow.po_qty = row.po_qty;
                        tRow.rece_qty = row.rece_qty;
                        tRow.acce_qty = row.acce_qty;
                        tRow.rej_qty = row.rej_qty;
                        tRow.pend_qty = row.pend_qty;
                        tRow.item_rate = row.item_rate;
                        tRow.sub_total = row.sub_total;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.item_value = row.item_value;
                        tRow.remarks = row.remarks;
                        DetailTable.Add(tRow);
                    }
                }
                DetailTable.Add(new GrinDetailModel { grin_detail_id = 0, grin_header_id = 0, material_id = 0, unit_code = 0, mfg_date = "", po_qty = Convert.ToDecimal("0.00"), pend_qty = Convert.ToDecimal("0.00"), rece_qty = Convert.ToDecimal("0.00"), acce_qty = Convert.ToDecimal("0.00"), rej_qty = Convert.ToDecimal("0.00"), item_rate = Convert.ToDecimal("0.00"), sub_total = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), item_value = Convert.ToDecimal("0.00"), remarks = "" });
                               
                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = DetailTable;
            }
            else if (btnAdd == "Add")
            {
                List<GrinTaxModel> TaxTable = new List<GrinTaxModel>();
                if (Model.GrinTax != null)
                {
                    foreach (var row in Model.GrinTax)
                    {
                        GrinTaxModel tRow = new GrinTaxModel();
                        tRow.tax_id = row.tax_id;
                        tRow.grin_header_id = row.grin_header_id;
                        tRow.acct_id = row.acct_id;
                        tRow.basic_amount = row.basic_amount;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.tax_amount = row.tax_amount;
                        TaxTable.Add(tRow);
                    }
                }
                TaxTable.Add(new GrinTaxModel { tax_id = 0, grin_header_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
                mymodel.GrinTax = TaxTable;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;
            }
            else if (btnAdd == "Save")
            {
                List<GrinTaxModel> TaxTable = Model.GrinTax;
                GrinHeaderModel header = Model.GrinHeaderSearch;
                header.Mode = 1;
                header.grin_header_id = lGH.NextId();
                header.grin_type = "WITHOUT PO";
                header.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());               
                header.financial_year = (string)Session["FinancialYear"];
                header.created_by = (string)Session["LoginUserName"];
                header.created_date = DateTime.Now;
                lGH.InsertUpdate(header);

                List<GrinDetailModel> detail = Model.GrinDetail;
                if (detail != null)
                {
                    foreach (var item in detail)
                    {
                        if (item.material_id != null)
                        {
                            ClsMaterialMaster lsmm = new ClsMaterialMaster();
                            MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + item.material_id + " ");
                            item.Mode = 1;
                            item.grin_detail_id = 0;
                            item.grin_header_id = header.grin_header_id;
                            item.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());
                            item.alt_unit = MM.alt_unit;
                            item.unit_code = MM.unit_code;
                            item.con_factor = MM.con_factor;
                            lGD.InsertUpdate(item);
                        }                        
                    }
                }

                //---------------------Add Voucher Sale Purchase--------------------------
                int liIndex = 3;
                double ldTaxAmt = 0;
                int liAcctId = lsPM.FindPartyid1(header.party_id);
                lsvoucher.AddSalePurchaseVouchers(1, header.grin_no, 1, Convert.ToDateTime(header.grin_date), "Purchase", "PURC", "GR", "C", liAcctId, 18, Math.Round(header.total_amount, 0), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, 2, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "GR", "D", liAcctId, 18, header.basic_amount, header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);

                foreach (GrinTaxModel dRow in TaxTable) // Tax entry
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "D", liAcctId, Convert.ToInt32(dRow.acct_id), Convert.ToDecimal(dRow.tax_amount), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                    ldTaxAmt += Convert.ToDouble(dRow.tax_amount);
                    liIndex++;
                }
                double liRoundOff = lsvoucher.GetRoundOffValue(Convert.ToDouble(header.basic_amount) + ldTaxAmt);
                if (liRoundOff < 0)
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "C", liAcctId, 122, Convert.ToDecimal(liRoundOff) * -1, header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                }
                else if (liRoundOff > 0)
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "D", liAcctId, 121, Convert.ToDecimal(liRoundOff), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                }
                else
                {
                    lsvoucher.AddSalePurchaseVouchers(2, header.grin_no, liIndex, Convert.ToDateTime(header.grin_date), "Purchase", "LDGR", "TX", "D", liAcctId, 121, Convert.ToDecimal(liRoundOff), header.grin_header_id, "Purchase Invoice Remarks", header.financial_year, header.created_by);
                }

                lsPH.PurchaseClose(Convert.ToInt32(header.grin_header_id), Convert.ToInt32(header.site_id));
               
                lsPH.StockDetail_Purchase(Convert.ToInt32(header.grin_header_id), Convert.ToInt32(header.site_id));

              
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (GrinTaxModel dRow in TaxTable)
                        {
                            dRow.grin_header_id = header.grin_header_id;
                             if (dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 1;
                                    lsGT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }
                ViewBag.Message = "Detail Save SuccessFully";
                
                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;
            }          
            return View(mymodel);
        }
        
        public ActionResult Edit(int id)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            GrinModel Model = new GrinModel();
            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();
            GrinHeaderModel header = new GrinHeaderModel();
            header=lGH.GrinHeader(id.ToString());
            Model.GrinDetail= lGD.GetGrinDetails1("grin_header_id = "+ id +" ");
            Model.GrinTax = lsGT.GetGrinTax("grin_header_id = " + id + " ");
            Model.GrinHeaderSearch = header;

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + "AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");
            ClsUnitMaster lsUom = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");

            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeaderList("purchase_header.party_id=" + Convert.ToInt32(Model.GrinHeaderSearch.party_id.ToString()) + " "), "po_id", "po_no");

            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ViewBag.Titlename = Model.GrinHeaderSearch.grin_type;
            return View(Model);
        }

        [HttpPost]
        public ActionResult Edit(GrinModel Model, FormCollection form, string btnAdd)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());           
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.ItemList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + "AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");

            ClsUnitMaster lsUom = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUom.UOMMaster(), "unit_code", "short_desc");

            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeaderList("purchase_header.party_id=" + Convert.ToInt32(Model.GrinHeaderSearch.party_id) + " "), "po_id", "po_no"); 

            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();
            GrinModel mymodel = new GrinModel();
            GrinHeaderModel header = Model.GrinHeaderSearch;

            if (btnAdd == "Add")
            {
                List<GrinTaxModel> TaxTable = new List<GrinTaxModel>();
                if (Model.GrinTax != null)
                {
                    foreach (var row in Model.GrinTax)
                    {
                        GrinTaxModel tRow = new GrinTaxModel();
                        tRow.tax_id = row.tax_id;
                        tRow.grin_header_id = row.grin_header_id;
                        tRow.acct_id = row.acct_id;
                        tRow.basic_amount = row.basic_amount;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.tax_amount = row.tax_amount;
                        TaxTable.Add(tRow);
                    }
                }
                TaxTable.Add(new GrinTaxModel { tax_id = 0, grin_header_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
                mymodel.GrinTax = TaxTable;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;
            }
            else if (btnAdd == "Save")
            {
                if (header.grin_header_id != 0)
                {
                    header.Mode = 2;
                    header.last_edited_by = (string)Session["LoginUserName"];
                    header.last_edited_date = DateTime.Now;
                    lGH.InsertUpdate(header);
                    List<GrinDetailModel> detail = Model.GrinDetail;
                    if (detail != null)
                    {
                        foreach (var item in detail)
                        {
                            ClsMaterialMaster lsmm = new ClsMaterialMaster();
                            MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + item.material_id + " ");
                            item.Mode = 2;
                            item.grin_header_id = header.grin_header_id;
                            item.alt_unit = MM.alt_unit;
                            item.unit_code = MM.unit_code;
                            item.con_factor = MM.con_factor;
                            lGD.InsertUpdate(item);
                        }
                    }
                    List<GrinTaxModel> TaxTable = Model.GrinTax;
                    if (TaxTable != null)
                    {
                        if (TaxTable.Count > 0)
                        {
                            foreach (GrinTaxModel dRow in TaxTable)
                            {
                                dRow.grin_header_id = header.grin_header_id;
                                if (dRow.tax_id > 0)
                                {
                                    dRow.Mode = 2;
                                    lsGT.InsertUpdate(dRow);
                                }
                                else if (dRow.tax_id == 0)
                                {
                                    dRow.Mode = 1;
                                    lsGT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }
                ViewBag.Message = "Detail Save SuccessFully";
                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;
            }
            return View(mymodel);
        }

        public JsonResult FillByPurchaseNo(string PartyNo, string PartyName)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            var result = new { List = new SelectList(lsPH.PurchaseHeaderList("purchase_header.party_id = " + Convert.ToInt32(PartyNo) + " and purchase_header.site_id = " + SiteId + " AND purchase_header.approval_flag = 'True' AND purchase_header.po_close_flag = 'False' "), "po_id", "po_no") };
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

        public JsonResult FillByUnitCode(string ItemCode, string ItemName, string PartyId, int CompanyId)
        {
            if (string.IsNullOrEmpty(ItemCode))
            {
                ItemCode = "0";
            }
            ClsMaterialMaster lsmm = new ClsMaterialMaster();
            MaterialMasterModel MM = new MaterialMasterModel();
            MM = lsmm.GSTMaterialList(" material_id =" + ItemCode + " ");

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

            int liSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsStStockHeader stockHeader = new ClsStStockHeader();
            var stock_qty = stockHeader.stStockQty(Convert.ToInt32(ItemCode), Convert.ToInt32(liSiteId));
            ClsUnitMaster lsUom = new ClsUnitMaster();
            var result = new { List = new SelectList(lsUom.GetUOMMaster(Convert.ToInt32(MM.alt_unit)), "unit_code", "short_desc"), MM.cgst, MM.sgst, MM.igst };
            return Json(result);
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

        public ActionResult GRN_Ticket()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string psFinancialYear = (string)Session["FinancialYear"];

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            string CompanyCode = lsCompany.FindCompanyCode(1);         
            
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ClsSaleOrderHeader lsOH = new ClsSaleOrderHeader();
            ViewBag.SaleHeader = new SelectList(lsOH.SaleHeaderList(), "order_id", "order_no");

            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");

            ClsSiteMaster lsSite = new ClsSiteMaster();
            string psSiteName = lsSite.FindSiteName(SiteId);

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
            
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeader(), "po_id", "po_no");

            ClsTickets lsTicket = new ClsTickets();
            ViewBag.TicketList= new SelectList(lsTicket.Tickets(), "ticket_number", "vehicle_number");

            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");

           
            GrinModel mymodel = new GrinModel();
            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsGrinTax lsGT = new ClsGrinTax();

            GrinHeaderModel header = new GrinHeaderModel();
            header.grin_header_id = lGH.NextId();          
            header.grin_no = string.Format("{0}/{1}/{2}/{3}", "GRN", CompanyCode, psFinancialYear, lGH.NextNoCompanywise(1, psFinancialYear));

            header.grin_date = DateTime.Now;
            header.inv_date = DateTime.Now;
            header.gate_date = DateTime.Now;
            mymodel.GrinHeaderSearch = header;
            mymodel.GrinTax = new List<GrinTaxModel>();
            mymodel.GrinTax.Add(new GrinTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });
            return View(mymodel);
        }
        [HttpPost]
        public ActionResult GRN_Ticket(GrinModel Model, FormCollection form, string btnAdd)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsGrinDetails lGD = new ClsGrinDetails();
            ClsTickets clsTickets = new ClsTickets();
            ClsGrinTax lsGT = new ClsGrinTax();
            GrinModel mymodel = new GrinModel();            

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("S"), "party_id", "party_name");
           
            ClsPurchaseHeader lsPH = new ClsPurchaseHeader();
            ViewBag.PurchaseHeader = new SelectList(lsPH.PurchaseHeaderList("purchase_header.party_id=" + Convert.ToInt32(Model.GrinHeaderSearch.party_id) + " and purchase_header.site_id = " + SiteId + " and purchase_header.po_close_flag = 'False' "), "po_id", "po_no");

            ClsTickets lsTicket = new ClsTickets();
            ViewBag.TicketList = new SelectList(lsTicket.GRNTicketList("tickets.party_id=" + Convert.ToInt32(Model.GrinHeaderSearch.party_id)+ " "), "ticket_number", "vehicle_number");

            ClsPayterm lsPT = new ClsPayterm();
            ViewBag.Payterm = new SelectList(lsPT.Payterm(), "payterm_code", "payterm_desc");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");
           
            if (btnAdd == "Add")
            {
                GrinHeaderModel Header = Model.GrinHeaderSearch;
                List<GrinTaxModel> TaxTable = new List<GrinTaxModel>();

                foreach (var row in Model.GrinTax)
                {
                    GrinTaxModel tRow = new GrinTaxModel();
                    tRow.tax_id = row.tax_id;
                    tRow.acct_id = row.acct_id;
                    tRow.basic_amount = row.basic_amount;
                    tRow.cgst = row.cgst;
                    tRow.sgst = row.sgst;
                    tRow.tax_amount = row.tax_amount;
                    TaxTable.Add(tRow);
                }
                TaxTable.Add(new GrinTaxModel { tax_id = 0, acct_id = 0, basic_amount = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), tax_amount = Convert.ToDecimal("0.00") });

                mymodel.GrinTax = TaxTable;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinDetail = Model.GrinDetail;                
            }

            else if (btnAdd == "Save")
            {
                GrinHeaderModel header = Model.GrinHeaderSearch;
                header.Mode = 1;
                header.grin_header_id = lGH.NextId();
                header.grin_type = "WITH Ticket";
                header.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());
                header.financial_year = (string)Session["FinancialYear"];
                header.created_by = (string)Session["LoginUserName"];
                header.created_date = DateTime.Now;
                lGH.InsertUpdate(header);

                List<GrinDetailModel> detail = Model.GrinDetail;
                if (detail != null)
                {
                    foreach (var item in detail)
                    {
                        if (item.is_select == true)
                        {
                            item.Mode = 1;
                            item.grin_detail_id = 0;
                            item.grin_header_id = header.grin_header_id;
                            item.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());
                            lGD.InsertUpdate(item);
                        }
                        clsTickets.UpdateGrnTicket(Convert.ToInt32(item.ticket_number));
                    }
                }
                
                lsPH.StockDetail_Purchase(Convert.ToInt32(header.grin_header_id), Convert.ToInt32(header.site_id));


                List<GrinTaxModel> TaxTable = Model.GrinTax;
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (GrinTaxModel dRow in TaxTable)
                        {
                            dRow.grin_header_id = header.grin_header_id;
                            if (dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != null)
                                {
                                    dRow.Mode = 1;
                                    lsGT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }
                ViewBag.Message = "Detail Save SuccessFully";
                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
               
                mymodel.GrinDetail = Model.GrinDetail;
            }
            else
            {
                ModelState.Clear();             
                mymodel.GrinDetail = lGD.FillByWithTickets(Convert.ToInt32(Model.GrinHeaderSearch.po_id));               
                ClsPurchaseHeader lsPheader = new ClsPurchaseHeader();
                PurchaseHeaderModel PurchaseModel = lsPheader.PurchaseHeader(Convert.ToInt32(Model.GrinHeaderSearch.po_id));
                Model.GrinHeaderSearch.po_date = PurchaseModel.po_date;
                Model.GrinHeaderSearch.company_id = Convert.ToInt32(PurchaseModel.company_id);

                ClsFinancialYear lsFinYear = new ClsFinancialYear();
                string psFinancialYear = lsFinYear.CurrentYear();
                string CompanyCode = lsCompany.FindCompanyCode(Model.GrinHeaderSearch.company_id);
                Model.GrinHeaderSearch.grin_no = string.Format("{0}/{1}/{2}/{3}", "GRN", CompanyCode, psFinancialYear, lGH.NextNoCompanywise(Model.GrinHeaderSearch.company_id, psFinancialYear));

                mymodel.GrinTax = Model.GrinTax;
                mymodel.GrinHeaderSearch = Model.GrinHeaderSearch;
                mymodel.GrinHeaderSearch.basic_amount = 0;
                mymodel.GrinHeaderSearch.total_amount = 0;
            }
           

            return View(mymodel);
        }

        //public JsonResult FillByTicketNo(string PartyId, string PartyName)
        //{
        //    GrinModel mymodel = new GrinModel();
        //    int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
        //    ClsTickets lsTicket= new ClsTickets();
        //    mymodel.TicketList = lsTicket.TicketGrn(Convert.ToInt32(PartyId));           
        //    return Json(mymodel.TicketList, JsonRequestBehavior.AllowGet);
        //}        

        public ActionResult GRNNO(int Company_id, string Company_name)
        {
            GrinHeaderModel Model = new GrinHeaderModel();
            ClsGrinHeader lGH = new ClsGrinHeader();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsFinancialYear lsFinYear = new ClsFinancialYear();
            string psFinancialYear = lsFinYear.CurrentYear();
            string CompanyCode = lsCompany.FindCompanyCode(Company_id);

            Model.grin_no = string.Format("{0}/{1}/{2}/{3}", "GRN", CompanyCode, psFinancialYear, lGH.NextNoCompanywise(Company_id, psFinancialYear));
            try
            {
                return Json(new
                {
                    msg = Model.grin_no
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
    }
}
