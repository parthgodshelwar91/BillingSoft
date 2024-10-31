using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
//using Aspose.Pdf;
//using iTextSharp.text.pdf;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;



namespace ProBillInvoice.Controllers
{
    //[Authorize]
    public class SaleInvoiceController : Controller
    {
        // GET: SaleInvoice
        
        //string CompanyCode = "SRMC";
        public ActionResult Index()
        {
            //string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, "04");
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now);
           
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");

            SaleInvoiceModel Model = new SaleInvoiceModel();            
            SaleInvoiceHeaderModel header = new SaleInvoiceHeaderModel();
            header.invoice_from = Convert.ToDateTime(lsFromDate);
            header.invoice_to = Convert.ToDateTime(lsToDate);
            
            Model.InvoiceHeaderSearch = header;

            string lsFilter = string.Empty;
            
            lsFilter = lsFilter + "sale_invoice_header.invoice_date Between '" + lsFromDate + "' and '" + lsToDate + "' ";

            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            Model.SaleInvoiceList = SIH.SaleInvoieHeaderList(lsFilter);

            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();       
            List<SaleInvoiceDetailModel> lsSD = new List<SaleInvoiceDetailModel>();
            List<SaleInvoiceDetailModel> lsSD2 = new List<SaleInvoiceDetailModel>();
            foreach (var item in Model.SaleInvoiceList)
            {
                //SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = SID.InoviceDetail(item.sale_invoice_id);
                lsSD2.AddRange(lsSD);
            }
            Model.InvoiceDetails = lsSD2;


            //-------- DM Pending In Invoice code below-----------------------------------------------------------------------------------------
            ViewBag.Titlename = "DM Pending In Invoice";                        
            ClsTickets_Reports lsTicket = new ClsTickets_Reports();                        
            string lsFilter1 = string.Empty;            
            lsFilter1 = lsFilter1 + "tickets.is_valid='false' and tickets.ticket_date_time between '" + lsFromDate + "' and '" + lsToDate + "' ";
            Model.TicketsList = lsTicket.DMClosed(lsFilter1);
           
            return View(Model);
        }

        [HttpPost]
        public ActionResult Index(SaleInvoiceModel Model)
        {          
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.InvoiceHeaderSearch.invoice_from);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Model.InvoiceHeaderSearch.invoice_to);
            int party_id = Convert.ToInt32(Model.InvoiceHeaderSearch.PartyId);
            string lsFilter = string.Empty;

            if (Convert.ToInt32(Model.InvoiceHeaderSearch.PartyId) != 0)
            {
                lsFilter = lsFilter + "sale_invoice_header.party_id = " + Convert.ToInt32(Model.InvoiceHeaderSearch.PartyId) + " and ";
            }           
            lsFilter = lsFilter + "sale_invoice_header.invoice_date Between '" + lsFromDate + "' and '" + lsToDate + "' ";

            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            Model.SaleInvoiceList = SIH.SaleInvoieHeaderList(lsFilter);

            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();
            List<SaleInvoiceDetailModel> lsSD = new List<SaleInvoiceDetailModel>();
            List<SaleInvoiceDetailModel> lsSD2 = new List<SaleInvoiceDetailModel>();
            foreach (var item in Model.SaleInvoiceList)
            {
                //SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = SID.InoviceDetail(item.sale_invoice_id);
                lsSD2.AddRange(lsSD);
            }
            Model.InvoiceDetails = lsSD2;

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");           
            return View(Model);
        }

        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string psFinancialYear = (string)Session["FinancialYear"];
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.CompanyList = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");
            int CompanyId = lsSM.FindCompanyId(Convert.ToInt32(SiteId));       
            string CompanyCode = lsCM.FindCompanyCode(CompanyId);

            ClsAccountMaster lsAM = new ClsAccountMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsSaleOrderHeader lsOH = new ClsSaleOrderHeader();                   
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");            
            ViewBag.SaleHeader = new SelectList(lsOH.SaleHeaderList(), "order_id", "order_no");
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CustLocation = new SelectList(lscustlocation.CustomerPartymaster(), "location_id", "location_detail");

            SaleInvoiceModel Model = new SaleInvoiceModel();
            SaleInvoiceHeaderModel headerModel = new SaleInvoiceHeaderModel();
            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceTaxes SIT = new ClsSaleInvoiceTaxes();
            Model.InvoiceHeaderSearch = SIH.SaleInvoieHeader(0);
            Model.InvoiceHeaderSearch.party_id = SIH.PartyName();           
            headerModel.sale_invoice_id=SIH.NextId();
            headerModel.company_id = CompanyId;            
            headerModel.invoice_no = string.Format("{0}/{1}/{2}/{3}", "SI", CompanyCode, psFinancialYear, SIH.NextNoCompanywise(CompanyId, psFinancialYear));

            headerModel.invoice_date = DateTime.Now;           
            Model.InvoiceHeaderSearch = headerModel;
            Model.SaleInvoiceList = SIH.SaleInvoieView(0);
            Model.InvoiceTax = new List<SaleInvoiceTaxesModel>();
            Model.InvoiceTax.Add(new SaleInvoiceTaxesModel { acct_id = 0, percentage = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), amount = Convert.ToDecimal("0.00") });  
            return View(Model);
        }

        [HttpPost]
        public ActionResult Create(SaleInvoiceModel Model,FormCollection form, string btnAdd)
        {
            int psSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());           

            SaleInvoiceHeaderModel header = new SaleInvoiceHeaderModel();
            List<SaleInvoiceDetailModel> detail = new List<SaleInvoiceDetailModel>();
            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes SIT = new ClsSaleInvoiceTaxes();
            ClsTickets lstickets = new ClsTickets();
            SaleInvoiceModel mymodel = new SaleInvoiceModel();
            if (btnAdd == "Add")
            {
                List<SaleInvoiceTaxesModel> TaxTable = new List<SaleInvoiceTaxesModel>();
                SaleInvoiceHeaderModel Header = Model.InvoiceHeaderSearch;                

                foreach (var row in Model.InvoiceTax)
                {
                    SaleInvoiceTaxesModel tRow = new SaleInvoiceTaxesModel();
                    tRow.acct_id = row.acct_id;
                    tRow.account_name = row.account_name;
                    tRow.percentage = row.percentage;
                    tRow.cgst = row.cgst;
                    tRow.sgst = row.sgst;
                    tRow.igst = row.igst;
                    tRow.amount = row.amount;                   
                    TaxTable.Add(tRow);
                }
                TaxTable.Add(new SaleInvoiceTaxesModel { acct_id = 0, account_name = "", percentage = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), amount = Convert.ToDecimal("0.00") });
                
                mymodel.InvoiceTax = TaxTable;
                mymodel.InvoiceHeaderSearch = Model.InvoiceHeaderSearch;
                mymodel.InvoiceDetails = Model.InvoiceDetails;
            }
            else if (btnAdd == "Save")
            {                
                List<SaleInvoiceTaxesModel> TaxTable = Model.InvoiceTax;
                header = Model.InvoiceHeaderSearch;
                detail = Model.InvoiceDetails;   
                
                header.Mode = 1;
                header.sale_invoice_id = SIH.NextId();
                header.invoice_type = "Direct";
                header.so_id = header.order_id;
                header.site_id = psSiteId;
                header.financial_year = (string)Session["FinancialYear"];
                header.created_by = (string)Session["LoginUserName"];
                header.created_date = DateTime.Now;
                //insert 
                SIH.InsertUpdate(header);

                ClsVoucherEntry lsvoucher = new ClsVoucherEntry();
                ClsPartyMaster ClsPM = new ClsPartyMaster();
                if (detail != null)
                {
                    if (detail.Count > 0)
                    {
                        foreach (SaleInvoiceDetailModel dRow in detail)
                        {
                            dRow.sale_invoice_id = header.sale_invoice_id;                           
                            dRow.ticket_date_time = Convert.ToDateTime(header.invoice_date);                         
                            dRow.party_id = header.party_id;
                            

                            if (dRow.is_select == true)
                            {                                
                                if (dRow.invoice_detail_id > 0)
                                {
                                    dRow.Mode = 2;
                                   SID.InsertUpdate(dRow);                                   
                                }
                                else if (dRow.invoice_detail_id == 0)
                                {
                                    ClsMaterialMaster lsmm = new ClsMaterialMaster();
                                    MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + dRow.material_id + " ");
                                    dRow.Mode = 1;
                                    dRow.alt_unit = MM.alt_unit;
                                    dRow.unit_code = MM.unit_code;
                                    dRow.con_factor = MM.con_factor;
                                    SID.InsertUpdate(dRow);                                                      
                                }
                                //---------------------Add Voucher Sale Purchase--------------------------
                                int liIndex = 3;
                                double ldTaxAmt = 0;
                                int liAcctId = ClsPM.FindPartyid1(header.party_id);
                                lsvoucher.AddSalePurchaseVouchers(1, header.invoice_no, 1, Convert.ToDateTime(dRow.ticket_date_time), "Sale", "SALE", "SR", "D", liAcctId, 38, Math.Round(header.total_amount, 0), header.sale_invoice_id, "Sale Invoice Remarks", header.financial_year, header.created_by);
                                lsvoucher.AddSalePurchaseVouchers(2, header.invoice_no, 2, Convert.ToDateTime(dRow.ticket_date_time), "Sale", "LDGR", "SR", "C", liAcctId, 38, header.basic_amount, header.sale_invoice_id, "Sale Invoice Remarks", header.financial_year, header.created_by);

                                foreach (SaleInvoiceTaxesModel item in TaxTable) // Tax entry
                                {
                                    lsvoucher.AddSalePurchaseVouchers(2, header.invoice_no, liIndex, Convert.ToDateTime(dRow.ticket_date_time), "Sale", "LDGR", "TX", "C", liAcctId, Convert.ToInt32(item.acct_id), Convert.ToDecimal(item.amount), header.sale_invoice_id, "Sale Invoice Remarks", header.financial_year, header.created_by);
                                    ldTaxAmt += Convert.ToDouble(item.amount);
                                    liIndex++;
                                }
                                double liRoundOff = lsvoucher.GetRoundOffValue(Convert.ToDouble(header.basic_amount) + ldTaxAmt);
                                if (liRoundOff < 0)
                                {
                                    lsvoucher.AddSalePurchaseVouchers(2, header.invoice_no, liIndex, Convert.ToDateTime(dRow.ticket_date_time), "Sale", "LDGR", "TX", "D", liAcctId, 122, Convert.ToDecimal(liRoundOff) * -1, header.sale_invoice_id, "Sale Invoice Remarks", header.financial_year, header.created_by);
                                }
                                else if (liRoundOff > 0)
                                {
                                    lsvoucher.AddSalePurchaseVouchers(2, header.invoice_no, liIndex, Convert.ToDateTime(dRow.ticket_date_time), "Sale", "LDGR", "TX", "C", liAcctId, 121, Convert.ToDecimal(liRoundOff), header.sale_invoice_id, "Sale Invoice Remarks", header.financial_year, header.created_by);
                                }
                                else
                                {
                                    lsvoucher.AddSalePurchaseVouchers(2, header.invoice_no, liIndex, Convert.ToDateTime(dRow.ticket_date_time), "Sale", "LDGR", "TX", "C", liAcctId, 121, Convert.ToDecimal(liRoundOff), header.sale_invoice_id, "Sale Invoice Remarks", header.financial_year, header.created_by);
                                }
                            }
                            lstickets.updateTickets(dRow.sale_invoice_id, 1);
                            SIH.SaleOrderClose_ForInvoice(header.order_id);
                        }
                    }
                }
                if (TaxTable != null)
                {
                    if (TaxTable.Count > 0)
                    {
                        foreach (SaleInvoiceTaxesModel dRow in TaxTable)
                        {
                            dRow.sale_invoice_id = header.sale_invoice_id;
                            if(dRow.tax_id == 0)
                            {
                                if (dRow.acct_id != 0)
                                {
                                    dRow.Mode = 1;
                                    SIT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                }
                ViewBag.Message = "Detail save successfully";
                mymodel = Model;
            }

            else
            {
                ModelState.Clear();
                int partyid = Model.InvoiceHeaderSearch.party_id;
                int order_id = Model.InvoiceHeaderSearch.order_id;              
                string lsFilter = string.Empty;
                if (Model.InvoiceHeaderSearch.party_id != 0)
                {
                    lsFilter = lsFilter + " tickets.party_id = " + Model.InvoiceHeaderSearch.party_id + " AND ";
                }
                if (Model.InvoiceHeaderSearch.order_id != 0)
                {
                    lsFilter = lsFilter + "tickets.order_id  = '" + Model.InvoiceHeaderSearch.order_id + "' AND ";
                }
                if (Model.InvoiceHeaderSearch.SiteId != 0)
                {
                    lsFilter = lsFilter + " tickets.godown_id = " + psSiteId + " AND ";
                }               
                lsFilter = lsFilter + "tickets.pending = 'False' AND tickets.closed = 'True' AND tickets.is_valid = 'False' ";

                ClsSaleInvoiceDetail lsDetail = new ClsSaleInvoiceDetail();
                Model.InvoiceDetails = lsDetail.FillByInvPendingTickets(lsFilter);

                mymodel.InvoiceDetails = Model.InvoiceDetails;
                mymodel.InvoiceHeaderSearch = Model.InvoiceHeaderSearch;
                mymodel.InvoiceTax = Model.InvoiceTax;              
                mymodel.InvoiceHeaderSearch.total_amount = 0;
                mymodel.InvoiceHeaderSearch.basic_amount = 0;
            }
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsSaleOrderHeader lsOH = new ClsSaleOrderHeader();
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ViewBag.SaleHeader = new SelectList(lsOH.PendingPartyInv_SaleHeaderList(Convert.ToInt32(Model.InvoiceHeaderSearch.party_id)), "order_id", "order_no");
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ViewBag.CompanyList = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CustLocation = new SelectList(lscustlocation.CustomerLocation(mymodel.InvoiceHeaderSearch.party_id), "location_id", "location_detail");

            return View("create", mymodel);
        }

        public ActionResult Edit(int Id)
        {
            int psSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            SaleInvoiceModel Model = new SaleInvoiceModel();            
            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes SIT = new ClsSaleInvoiceTaxes();
            Model.InvoiceHeaderSearch = SIH.SaleInvoieHeader(Id);
            Model.InvoiceHeaderSearch.order_id = Model.InvoiceHeaderSearch.so_id;
            Model.InvoiceHeaderSearch.SiteId = Model.InvoiceHeaderSearch.site_id; 
            Model.InvoiceHeaderSearch.customer_location_id = Model.InvoiceHeaderSearch.customer_location_id;
            Model.InvoiceDetails = SID.InoviceDetail1(Id);
            Model.InvoiceTax = SIT.InvoiceTaxes("sale_invoice_id= "+ Id + " ");
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsSaleOrderHeader lsOH = new ClsSaleOrderHeader();
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            
            ViewBag.SaleHeader = new SelectList(lsOH.PendingPartyInv_SaleHeaderList(Convert.ToInt32(Model.InvoiceHeaderSearch.party_id)), "order_id", "order_no");
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ViewBag.CompanyList = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CustLocation = new SelectList(lscustlocation.CustomerLocation(Model.InvoiceHeaderSearch.party_id), "location_id", "location_detail");
            Session["TaxTable"] = Model.InvoiceTax;
            return View(Model);
        }

        [HttpPost]
        public ActionResult Edit(SaleInvoiceModel Inv, FormCollection form, string btnAdd)  //, FormCollection form, string btnAdd, string item_code, int unit_code, string ItemQty, string ItemRate
        {
            int psSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsAccountMaster lsAM = new ClsAccountMaster();
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ClsSaleOrderHeader lsOH = new ClsSaleOrderHeader();
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");
            ViewBag.AccountList = new SelectList(lsAM.AccountMasterList("account_mst.parent_id in(28,17,37) AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ViewBag.SaleHeader = new SelectList(lsOH.SaleHeaderList(), "order_id", "order_no");
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(0), "site_id", "site_name");
            ViewBag.CompanyList = new SelectList(lsCM.CompanyMaster(), "company_id", "company_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CustLocation = new SelectList(lscustlocation.CustomerLocation(Inv.InvoiceHeaderSearch.party_id), "location_id", "location_detail");

            SaleInvoiceHeaderModel Header = Inv.InvoiceHeaderSearch;
            List<SaleInvoiceDetailModel> DetailTable = Inv.InvoiceDetails;
            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes SIT = new ClsSaleInvoiceTaxes();
            SaleInvoiceModel mymodel = new SaleInvoiceModel();
            ClsTickets lstickets = new ClsTickets();
            if (btnAdd == "Add")
            {
                List<SaleInvoiceTaxesModel> TaxTable = new List<SaleInvoiceTaxesModel>();              

                foreach (var row in Inv.InvoiceTax)
                {
                    SaleInvoiceTaxesModel tRow = new SaleInvoiceTaxesModel();
                    tRow.acct_id = row.acct_id;
                    tRow.account_name = row.account_name;
                    tRow.percentage = row.percentage;
                    tRow.cgst = row.cgst;
                    tRow.sgst = row.sgst;
                    tRow.igst = row.igst;
                    tRow.amount = row.amount;
                    TaxTable.Add(tRow);
                }
                TaxTable.Add(new SaleInvoiceTaxesModel { acct_id = 0, account_name = "", percentage = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), amount = Convert.ToDecimal("0.00") });

                mymodel.InvoiceTax = TaxTable;
                mymodel.InvoiceHeaderSearch = Inv.InvoiceHeaderSearch;
                mymodel.InvoiceDetails = Inv.InvoiceDetails;
            }
            
            else if (btnAdd == "Save")
            {
                List<SaleInvoiceTaxesModel> TaxTable = new List<SaleInvoiceTaxesModel>();
                TaxTable = Inv.InvoiceTax;
                if (Header.sale_invoice_id != 0)
                {
                    Header.Mode = 2;
                    Header.invoice_type = "Direct";
                    Header.so_id = Header.order_id;
                    Header.financial_year = (string)Session["FinancialYear"];
                    Header.last_edited_by = (string)Session["LoginUserName"];
                    Header.last_edited_date = DateTime.Now.Date;
                    SIH.InsertUpdate(Header);
                    //Invoice Details      
                    if (DetailTable != null)
                    {
                        if (DetailTable.Count > 0)
                        {
                            foreach (SaleInvoiceDetailModel dRow in DetailTable)
                            {                                
                                if (dRow.is_select == true)
                                {                                    
                                    if (dRow.invoice_detail_id > 0)
                                    {
                                        ClsMaterialMaster lsmm = new ClsMaterialMaster();
                                        MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + dRow.material_id + " ");
                                        dRow.Mode = 2;
                                        dRow.alt_unit = MM.alt_unit;
                                        dRow.unit_code = MM.unit_code;
                                        dRow.con_factor = MM.con_factor;
                                        SID.InsertUpdate( dRow);                                       
                                    }
                                    else if (dRow.invoice_detail_id == 0)
                                    {
                                        dRow.Mode = 1;
                                        SID.InsertUpdate(dRow);                                        
                                    }
                                }
                                lstickets.updateTickets(dRow.sale_invoice_id, 1);
                            }
                        }
                    }
                    if (TaxTable != null)
                    {
                        if (TaxTable.Count > 0)
                        {
                            foreach (SaleInvoiceTaxesModel dRow in TaxTable)
                            {
                                dRow.sale_invoice_id = Header.sale_invoice_id;
                                if (dRow.tax_id > 0)
                                {
                                    dRow.Mode = 2;
                                    SIT.InsertUpdate(dRow);
                                }
                                else if (dRow.tax_id == 0)
                                {
                                    dRow.Mode = 1;
                                    SIT.InsertUpdate(dRow);
                                }
                            }
                        }
                    }
                    ViewBag.Message = "Detail save successfully";
                }
            }
            
            mymodel.InvoiceHeaderSearch = Header;
            mymodel.InvoiceDetails = DetailTable;
            return View(mymodel);
        }
        
        public ActionResult PartyAddress(int id, string name)
        {
            ClsPartyMaster lsPM = new ClsPartyMaster();
            string lsAddress = lsPM.BillingAddress(id);
            ClsSaleOrderHeader SOH = new ClsSaleOrderHeader();
            List<SaleOrderHeaderModel> lsOrderList = new List<SaleOrderHeaderModel>();
            lsOrderList = SOH.PendingPartyInv_SaleHeaderList(id);
            ClsPartyMstCustLocation PMCL = new ClsPartyMstCustLocation();
            List<PartyMstCustLocationModel> lsCustomerList = new List<PartyMstCustLocationModel>();
            lsCustomerList = PMCL.CustomerLocation(id);
            var result = new { lsOrderList = new SelectList(lsOrderList, "order_id", "order_no"), lsCustomerList = new SelectList(lsCustomerList, "location_id", "city_name") };
            return Json(result, JsonRequestBehavior.AllowGet);           
        }

        public ActionResult InvoiceNo(int id, string name)
        {
            int CompanyId = id;
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            string CompanyCode = lsCM.FindCompanyCode(CompanyId);
            string psFinancialYear = (string)Session["FinancialYear"];

            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();                       
            string invNo = string.Format("{0}/{1}/{2}/{3}", "SI", CompanyCode, psFinancialYear, SIH.NextNoCompanywise(CompanyId, psFinancialYear));
            return Json(invNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartyDetail(int PartyId, string PartyName)
        {
            ClsPartyMaster lsPM = new ClsPartyMaster();
            string PartyCodeAddress = lsPM.FillByPartyId(PartyId, "Customer");

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

        public ActionResult CustomerLocation(int PartyId, string PartyName)
        {
            ClsPartyMstCustLocation PMCL = new ClsPartyMstCustLocation();
            List<PartyMstCustLocationModel> lsCustomerList = new List<PartyMstCustLocationModel>();
            lsCustomerList = PMCL.CustomerLocation(PartyId);
            var result = new { lsCustomerList = new SelectList(lsCustomerList, "id", "location_detail") };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerSaleOrder(int PartyId, string PartyName)
        {
            ClsSaleOrderHeader SOH = new ClsSaleOrderHeader();
            List<SaleOrderHeaderModel> lsOrderList = new List<SaleOrderHeaderModel>();
            lsOrderList = SOH.PendingPartyInv_SaleHeaderList(PartyId);            
            var result = new { lsOrderList = new SelectList(lsOrderList, "order_id", "order_no") };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Email(int Id)
        {            
            DataTable dt = new DataTable();
            SaleInvoiceModel Model = new SaleInvoiceModel();
            ClsSaleInvoiceHeader SIH = new ClsSaleInvoiceHeader();
            ClsSaleInvoiceDetail SID = new ClsSaleInvoiceDetail();
            Model.InvoiceHeaderSearch = SIH.SaleInvoieHeader(Id);
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");

            Model.SaleInvoice = SID.SaleInvoiceReport(Id);           
            string cardFileName = GeneratePdf(Model);
            //string cardFileName = htmltopdf(Model); 
            //ClsEmail mail = new ClsEmail();
            //mail.EmailSend(Model, cardFileName);
            ClsSendMail lsmail = new ClsSendMail();
            //lsmail.EmailSend(Model);

            //lsmail.SendMail();
            return View("Index",Model);
        }

      
        public ActionResult SaleInvoiceReport(SaleInvoiceDetailModel model)
        {
                         
            SaleInvoiceModel mymodel = new SaleInvoiceModel();                    
            ClsSaleInvoiceDetail lsDetail = new ClsSaleInvoiceDetail();
            ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            ClsCompanyMaster CM = new ClsCompanyMaster();        
            mymodel.SaleInvoice = lsDetail.SaleInvoiceReport(model.sale_invoice_id);           
            return View(mymodel);
        }
       

        public string GeneratePdf(SaleInvoiceModel model)
        {
       //     //string domainName = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
       //     string scheme = HttpContext.Request.Url.Scheme;           
       //     string URL = @Url.Action("SaleInvoiceReport", "SaleInvoice", new { model.SaleInvoice.sale_invoice_id }, Request.Url.Scheme);         
       //     //Create request for given url
       //     HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
       //     //Create response-object
       //     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
       //     //Take response stream
       //     StreamReader sr = new StreamReader(response.GetResponseStream());
       //     //Read response stream (html code)
       //     string htmlString = sr.ReadToEnd();
       //     //Close streamreader and response
       //     sr.Close();
       //     response.Close();
       //     //PocketICard Size Height = 3.375 inches  Width = 2.125 inches
            
       //     MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlString));
       //     HtmlLoadOptions options = new HtmlLoadOptions(URL);
       //     options.PageInfo.IsLandscape = false;
       //     options.PageInfo.Width = 306;   // in points 72pt = 1 inch
       //     options.PageInfo.Height = 243;  // in points 72pt = 1 inch
       //     options.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);
       //     string saveLocation = Server.MapPath("~/Doc_Files/");
       //     string filename = "Invoice_" + model.SaleInvoice.sale_invoice_id + ".pdf";
       //     //string filename = "Invoice_demo.pdf";
            string filepath = "";

       //     // Load HTML file
       //     //Document pdfDocument = new Document(stream, options);
       //     Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(stream,options);
       //     //pdfDocument.Pages[1].Rotate = Rotation.on180;
       //     pdfDocument.Save(filepath);
       //// }
            return filepath;
        }       
    }
}