using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class SaleOrderController : Controller
    {       
        public ActionResult Index()
        {

            //string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, "04");
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");
            ClsPartyMaster lsParty = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsParty.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            
            SaleOrderModel mymodel = new SaleOrderModel();
            SaleOrderHeaderModel SaleHeader = new SaleOrderHeaderModel();           
            SaleHeader.FromDate = Convert.ToDateTime(lsFromDate);
            SaleHeader.ToDate = Convert.ToDateTime(lsToDate);
            SaleHeader.SiteId = SiteId;
            mymodel.SaleOrderHeaderSearch = SaleHeader;

            string lsFilter = string.Empty;
            if (SiteId != 0)
            {
                lsFilter = lsFilter + "sale_order_header.site_id=" + SiteId + " and ";
            }
            lsFilter = lsFilter + "sale_order_header.order_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            ClsSaleOrderHeader lsSaleHeader = new ClsSaleOrderHeader();
            ClsSaleOrderDetail lsSaleDetail = new ClsSaleOrderDetail();
            mymodel.SaleOrderHeader = lsSaleHeader.SaleHeader(lsFilter);
            List<SaleOrderDetailModel> lsSD = new List<SaleOrderDetailModel>();
            List<SaleOrderDetailModel> lsSD2 = new List<SaleOrderDetailModel>();
            foreach (var item in mymodel.SaleOrderHeader)
            {
                //SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = lsSaleDetail.SaleOrderMaterial(item.order_id);
                lsSD2.AddRange(lsSD);               
            }
            mymodel.SaleOrderDetail = lsSD2;
            return View("Index", mymodel);
        }

        [HttpPost]
        public ActionResult Search(SaleOrderModel SOM)
        {
            string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", SOM.SaleOrderHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", SOM.SaleOrderHeaderSearch.ToDate);
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSM.SiteMaster(Convert.ToInt32(SiteId)), "site_id", "site_name");
            ClsPartyMaster lsParty = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsParty.FillByCategoryPartyMaster("C"), "party_id", "party_name");
           
            string lsFilter = string.Empty;
            if (SOM.SaleOrderHeaderSearch.SiteId != 0)
            {
                lsFilter = lsFilter + "sale_order_header.site_id = " + SOM.SaleOrderHeaderSearch.SiteId + " and ";
            }
            if (SOM.SaleOrderHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + " sale_order_header. party_id = " + SOM.SaleOrderHeaderSearch.PartyId + " AND ";
            }
            if (SOM.SaleOrderHeaderSearch.order_close == true)
            {
                lsFilter = lsFilter + "sale_order_header.order_close = '" + SOM.SaleOrderHeaderSearch.order_close + "' AND ";
            }
            if (SOM.SaleOrderHeaderSearch.order_close == false)
            {
                lsFilter = lsFilter + "sale_order_header.order_close = '" + SOM.SaleOrderHeaderSearch.order_close + "' AND ";
            }            
            lsFilter = lsFilter + "sale_order_header.order_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            SaleOrderModel mymodel = new SaleOrderModel();
            ClsSaleOrderHeader lsSaleHeader = new ClsSaleOrderHeader();
            mymodel.SaleOrderHeader = lsSaleHeader.SaleHeader(lsFilter);

            //-------SaleOrder-Material Search----------
            List<SaleOrderDetailModel> lsSD = new List<SaleOrderDetailModel>();
            List<SaleOrderDetailModel> lsSD2 = new List<SaleOrderDetailModel>();
            ClsSaleOrderDetail lsSaleDetail = new ClsSaleOrderDetail();
            foreach (var item in mymodel.SaleOrderHeader)
            {
                SaleOrderDetailModel saleOrder = new SaleOrderDetailModel();
                lsSD = lsSaleDetail.SaleOrderMaterial(item.order_id);
                lsSD2.AddRange(lsSD);
            }
            mymodel.SaleOrderDetail = lsSD2;
            return View("Index", mymodel);
        }

        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string psFinancialYear = Session["FinancialYear"].ToString();

            ClsPartyMaster lsParty = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsParty.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CityList = new SelectList(lscustlocation.CustomerPartymaster(), "id", "location_detail");           
            ClsAccountMaster lsAccount = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAccount.AccountMasterList("account_mst.parent_id = 31 AND account_mst.group_id not in(5) AND account_mst.defunct = 'false' "), "acct_id", "account_name");
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster_Type("Sale"), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");

            ClsSiteMaster lsSM = new ClsSiteMaster();
            int company_id = lsSM.FindCompanyId(Convert.ToInt32(SiteId));

            SaleOrderModel mymodel = new SaleOrderModel();
            SaleOrderHeaderModel HeaderModel = new SaleOrderHeaderModel();
            SaleOrderDetailModel DetailModel = new SaleOrderDetailModel();
            ClsSaleOrderHeader lsSaleHeader = new ClsSaleOrderHeader();
            ClsSaleOrderDetail lsSaleDetail = new ClsSaleOrderDetail();
            HeaderModel.order_date = DateTime.Now;
            HeaderModel.order_id = lsSaleHeader.NextId();
            HeaderModel.order_no = string.Format("{0}-{1}","SO", lsSaleHeader.NextNoStorewise(psFinancialYear));
            HeaderModel.company_id = company_id;
            mymodel.SaleOrderHeaderSearch = HeaderModel;
            mymodel.SaleOrderDetail = lsSaleDetail.SaleDetail(0);
          
            mymodel.SaleOrderDetail = new List<SaleOrderDetailModel>();
            mymodel.SaleOrderDetail.Add(new SaleOrderDetailModel { order_detail_id = 0, order_id = 0, material_id=0, unit_code=0, order_qty = Convert.ToDecimal("0.00"), item_rate = Convert.ToDecimal("0.00"), sub_total = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), final_item_rate = Convert.ToDecimal("0.00"), item_value = Convert.ToDecimal("0.00") });
          
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Create(SaleOrderModel SOM, string btnAdd, string btndeleteSO) 
        {
            string psFinancialYear = Session["FinancialYear"].ToString();
            ClsPartyMaster lsParty = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsParty.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CityList = new SelectList(lscustlocation.CustomerLocation(SOM.SaleOrderHeaderSearch.party_id), "id", "location_detail");           
            ClsAccountMaster lsAccount = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAccount.AccountMaster(), "acct_id", "account_name");
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster_Type("Sale"), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");
            
            SaleOrderModel mymodel = new SaleOrderModel();
            switch (btndeleteSO)
            {
                
                case string number when int.TryParse(number, out var index):
                    if (SOM.SaleOrderDetail.Count > 0)
                    {
                        SOM.SaleOrderDetail.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.SaleOrderDetail = SOM.SaleOrderDetail;                   
                    mymodel.SaleOrderHeaderSearch = SOM.SaleOrderHeaderSearch;
                    break;
            }

            if (btnAdd == "Add")
            {
                List<SaleOrderDetailModel> detail = new List<SaleOrderDetailModel>();
                if (SOM.SaleOrderDetail != null)
                {
                    foreach (var row in SOM.SaleOrderDetail)
                    {
                        SaleOrderDetailModel tRow = new SaleOrderDetailModel();
                        tRow.order_detail_id = row.order_detail_id;
                        tRow.order_id = row.order_id;
                        tRow.material_id = row.material_id;
                        tRow.unit_code = row.unit_code;
                        tRow.item_rate = row.item_rate;
                        tRow.sub_total = row.sub_total;
                        tRow.cgst = row.cgst;
                        tRow.sgst = row.sgst;
                        tRow.igst = row.igst;
                        tRow.item_value = row.item_value;
                        detail.Add(tRow);
                    }
                }
                detail.Add(new SaleOrderDetailModel { order_detail_id = 0, order_id = 0, material_id = 0, unit_code = 0, order_qty = Convert.ToDecimal("0.00"), item_rate = Convert.ToDecimal("0.00"), sub_total = Convert.ToDecimal("0.00"), cgst = Convert.ToDecimal("0.00"), sgst = Convert.ToDecimal("0.00"), igst = Convert.ToDecimal("0.00"), final_item_rate = Convert.ToDecimal("0.00"), item_value = Convert.ToDecimal("0.00") });

                mymodel.SaleOrderDetail = detail;
                mymodel.SaleOrderHeaderSearch = SOM.SaleOrderHeaderSearch;
            }

            else if (btnAdd == "Save")
            {                
                ClsSaleOrderHeader lsSaleheader = new ClsSaleOrderHeader();  
                SaleOrderHeaderModel HeaderModel = SOM.SaleOrderHeaderSearch;
                List<SaleOrderDetailModel> DetailTable= SOM.SaleOrderDetail;
                ClsSaleOrderDetail lsDetail = new ClsSaleOrderDetail();
                HeaderModel.order_id = lsSaleheader.NextId();

                if (HeaderModel.order_id != 0)
                {
                    HeaderModel.Mode = 1;                    
                    HeaderModel.financial_year = psFinancialYear;
                    HeaderModel.order_type = "O";
                    HeaderModel.location_id = lscustlocation.LocationId(HeaderModel.cust_site_location_id);
                    HeaderModel.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());                  
                    HeaderModel.delivery_date = DateTime.Now.Date.AddDays(10);
                    HeaderModel.created_by = User.Identity.Name; 
                    HeaderModel.created_date = DateTime.Now;                             
                    lsSaleheader.InsertUpdate(HeaderModel);
                    
                    if (DetailTable.Count > 0)
                    {
                        foreach (SaleOrderDetailModel tRow in DetailTable)
                        {
                            tRow.order_id = HeaderModel.order_id;                          
                            tRow.is_pending = true;                           
                           
                            if (tRow.order_detail_id == 0)
                            {
                                tRow.Mode = 1;
                                lsDetail.InsertUpdate(tRow);                                
                            }
                        }
                    }
                }
                ViewBag.Message = "Your Detail Save Sucessfully";
                mymodel.SaleOrderDetail = DetailTable;
            }
            
            return View(mymodel);
        }


        public ActionResult Edit(int id)
        {
            SaleOrderModel mymodel = new SaleOrderModel();
            ClsSaleOrderHeader lsSaleHeader = new ClsSaleOrderHeader();
            ClsSaleOrderDetail lsSaleDetail = new ClsSaleOrderDetail();

            mymodel.SaleOrderHeaderSearch = lsSaleHeader.SaleHeader(id);
            mymodel.SaleOrderDetail = lsSaleDetail.SaleDetail(id);
            mymodel.SaleOrderHeader = lsSaleHeader.SaleHeaderList();
            Session["DetailTable"] = mymodel.SaleOrderDetail;

            ClsPartyMaster lsParty = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsParty.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CityList = new SelectList(lscustlocation.CustomerLocation(mymodel.SaleOrderHeaderSearch.party_id), "id", "location_detail");            
            ClsAccountMaster lsAccount = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAccount.AccountMaster(), "acct_id", "account_name");
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster_Type("Sale"), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");

            
            return View( mymodel);
        }

        [HttpPost]
        public ActionResult Edit(SaleOrderModel SOM, FormCollection form, string btnAdd)  
        {
            string psFinancialYear = Session["FinancialYear"].ToString();

            ClsPartyMaster lsParty = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsParty.FillByCategoryPartyMaster("C"), "party_id", "party_name");
            ClsPartyMstCustLocation lscustlocation = new ClsPartyMstCustLocation();
            ViewBag.CityList = new SelectList(lscustlocation.CustomerPartymaster(), "id", "location_detail");            
            ClsAccountMaster lsAccount = new ClsAccountMaster();
            ViewBag.AccountList = new SelectList(lsAccount.AccountMaster(), "acct_id", "account_name");
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster_Type("Sale"), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");
            
            SaleOrderModel mymodel = new SaleOrderModel(); 

             if (btnAdd == "Save")
            {
                ClsSaleOrderHeader lsSaleheader = new ClsSaleOrderHeader();               
                SaleOrderHeaderModel HeaderModel = SOM.SaleOrderHeaderSearch;  
                if (HeaderModel.order_id != 0)
                {
                    HeaderModel.Mode = 2;                    
                    HeaderModel.order_type = "O";
                    HeaderModel.site_id = Convert.ToInt32(Session["LoginSiteId"].ToString());
                    HeaderModel.last_edited_by = User.Identity.Name;
                    HeaderModel.last_edited_date = DateTime.Now;
                    HeaderModel.financial_year = psFinancialYear;

                    lsSaleheader.InsertUpdate(HeaderModel);
                     
                    List <SaleOrderDetailModel> DetailTable= SOM.SaleOrderDetail;
                    if (DetailTable.Count > 0)
                    {
                        foreach (SaleOrderDetailModel tRow in DetailTable)
                        {                            
                            tRow.order_id = HeaderModel.order_id;                          
                                                   
                            ClsSaleOrderDetail lsDetail = new ClsSaleOrderDetail();
                            if (tRow.order_detail_id == 0)
                            {
                                tRow.Mode = 1;                              
                                lsDetail.InsertUpdate(tRow);                               
                            }
                            else if(tRow.order_detail_id != 0)
                            {
                                tRow.Mode = 2;
                                lsDetail.InsertUpdate(tRow);
                            }
                        }
                    }
                    mymodel.SaleOrderHeaderSearch = SOM.SaleOrderHeaderSearch;
                    mymodel.SaleOrderDetail = DetailTable;                    
                }
                ViewBag.Message = "Order Details Saved Sucessfully";
            }
           
            return View(mymodel);
        }

        public ActionResult PartyRecords(string PartyId, string PartyName)
        {
            ClsSaleOrderHeader lsHeader = new ClsSaleOrderHeader();
            string PartyRecords = lsHeader.FillByParty(Convert.ToInt32(PartyId));

            try
            {
                return Json(new
                {
                    msg = PartyRecords
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Records(int id, string name)
        {
            ClsSaleOrderDetail lsDetail = new ClsSaleOrderDetail();
            string lsRecords = lsDetail.Records(id);

            try
            {
                return Json(new
                {
                    msg = lsRecords
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

        public JsonResult FillByUnitCode(string ItemCode, string ItemName, int PartyId, string PartyName, int CompanyId)
        {
            ClsMaterialMaster lsmm = new ClsMaterialMaster();
            MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + ItemCode + " ");

            ClsPartyMaster lsPM = new ClsPartyMaster();            
            ClsCompanyMaster lsCM = new ClsCompanyMaster();         
            int PartyStateId = lsPM.FindStateId(PartyId);
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
            var result = new { List = new SelectList(lsUom.GetUOMMaster(Convert.ToInt32(MM.alt_unit)), "unit_code", "long_desc"), MM.cgst, MM.sgst, MM.igst };
            return Json(result);
        }

        public ActionResult SaleOrderReport(int Id, string Company_Id)
        {
            string lsFilter = string.Empty;
            SaleOrderModel mymodel = new SaleOrderModel();

            lsFilter = "sale_order_detail.order_id  = " + Id + " and ";
            lsFilter = lsFilter + "sale_order_detail.order_id IS Not NULL ";
            ClsSaleOrderDetail lsDetail = new ClsSaleOrderDetail();
            ClsSaleInvoiceTaxes lsTax = new ClsSaleInvoiceTaxes();
            ClsCompanyMaster CM = new ClsCompanyMaster();
            mymodel.Company = CM.CompanyMaster(Company_Id);
            mymodel.SaleOrderDetail = lsDetail.SaleOrderReport(lsFilter);
            //mymodel.InvoiceTax = lsTax.InvoiceTax(Id);
            return View(mymodel);
        }
    }
}