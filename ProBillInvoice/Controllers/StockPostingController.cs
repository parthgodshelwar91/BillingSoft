//using ClosedXML.Excel;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class StockPostingController : Controller
    {
        // GET: StockPosting
        public ActionResult Index()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");  
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");
                                    
            ClsStStockHeader lsStockHeader = new ClsStStockHeader();
            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            mymodel.StStockHeader = lsStockHeader.StStockHeader("st_stock_header.st_stock_header_id = 0");
            StStockHeaderModel header = new StStockHeaderModel();
            header.SiteId = SiteId;
            mymodel.StStockHeader = lsStockHeader.StStockHeader("st_stock_header.site_id = " + SiteId + " ");
            mymodel.StStockHeaderSearch = header;
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Search(StStockHeader_StockPostingModel SSH)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFilter = string.Empty;
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");            
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");          
            
            if (SSH.StStockHeaderSearch.SiteId != null)
            {
                lsFilter = lsFilter + "st_stock_header.site_id = " + SSH.StStockHeaderSearch.SiteId + " and ";
            }
            if (SSH.StStockHeaderSearch.MaterialId != null)
            {
                lsFilter = lsFilter + "st_stock_header.material_id = " + SSH.StStockHeaderSearch.MaterialId + " and ";
            }            
            if (SSH.StStockHeaderSearch.StoreId != null)
            {
                lsFilter = lsFilter + "st_stock_header.store_id = " + SSH.StStockHeaderSearch.StoreId + " and ";
            }
            if (SSH.StStockHeaderSearch.RackId != null)
            {
                lsFilter = lsFilter + "st_stock_header.rack_id = " + SSH.StStockHeaderSearch.RackId + " and ";
            }

            lsFilter = lsFilter + "st_stock_header.st_stock_header_id is not null ";
            Session["lsFilter"] = lsFilter;
            ClsStStockHeader lsStockHeader = new ClsStStockHeader();
            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            mymodel.StStockHeader = lsStockHeader.StStockHeader(lsFilter);
            return View("Index", mymodel);
        }


        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();            
            ViewBag.MaterialList = new SelectList(lsMaterial.FillByPosting_MaterialMaster("group_code in('02') AND material_id not in(select material_id from st_stock_header where site_id = " + SiteId + ")"), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");
                        
            ViewBag.lblUnitCode = "*";
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");

            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            StStockHeaderModel StockHeader = new StStockHeaderModel();
            ClsStStockHeader lsStockHeader = new ClsStStockHeader();
            StockHeader.st_stock_header_id = lsStockHeader.NextId();
            mymodel.StStockHeader = new List<StStockHeaderModel>();
            mymodel.StStockHeader.Add(new StStockHeaderModel { st_stock_header_id = 0, material_id = 0, brand_id = 0, store_id = 0, unit_code = 0, site_id = SiteId, rack_id = 0, opening_qty = Convert.ToDecimal("0.00"), total_rec_qty = Convert.ToDecimal("0.00"), total_iss_qty = Convert.ToDecimal("0.00"), total_balance = Convert.ToDecimal("0.00"), re_order = Convert.ToDecimal("0.00"), min_level = Convert.ToDecimal("0.00"), max_level= Convert.ToDecimal("0.00"), item_avg_rate= Convert.ToDecimal("0.00") });                       
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult Create(StStockHeader_StockPostingModel Model, string btnAdd,string btndeleteSP)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());                       

            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();          
            ViewBag.MaterialList = new SelectList(lsMaterial.FillByPosting_MaterialMaster("group_code in('02') AND material_id not in(select material_id from st_stock_header where site_id = " + SiteId + ")"), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");                                           

            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();

            switch (btndeleteSP)
            {                
                case string number when int.TryParse(number, out var index):
                    if (Model.StStockHeader.Count > 0)
                    {
                        Model.StStockHeader.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.StStockHeader = Model.StStockHeader;
                   
                    break;
            }

            if (btnAdd == "Add")
            {               
                List<StStockHeaderModel> Header = new List<StStockHeaderModel>();
                if (Model.StStockHeader != null)
                {
                    foreach (var row in Model.StStockHeader)
                    {
                        StStockHeaderModel tRow = new StStockHeaderModel();
                        tRow.st_stock_header_id = row.st_stock_header_id;
                        tRow.material_id = row.material_id;
                        tRow.brand_id = row.brand_id;
                        tRow.unit_code = row.unit_code;
                        tRow.store_id = row.store_id;
                        tRow.unit_code = row.unit_code;
                        tRow.site_id = row.site_id;
                        tRow.rack_id = row.rack_id;
                        tRow.opening_qty = row.opening_qty;
                        tRow.total_rec_qty = row.total_rec_qty;
                        tRow.total_iss_qty = row.total_iss_qty;
                        tRow.total_balance = row.total_balance;
                        tRow.re_order = row.re_order;
                        tRow.min_level = row.min_level;
                        tRow.max_level = row.max_level;
                        tRow.item_avg_rate = row.item_avg_rate;
                        Header.Add(tRow);
                    }
                }
                Header.Add(new StStockHeaderModel { st_stock_header_id = 0, material_id = 0, brand_id = 0, store_id = 0, unit_code = 0, site_id = SiteId, rack_id = 0, opening_qty = Convert.ToDecimal("0.00"), total_rec_qty = Convert.ToDecimal("0.00"), total_iss_qty = Convert.ToDecimal("0.00"), total_balance = Convert.ToDecimal("0.00"), re_order = Convert.ToDecimal("0.00"), min_level = Convert.ToDecimal("0.00"), max_level = Convert.ToDecimal("0.00"), item_avg_rate = Convert.ToDecimal("0.00") });

                mymodel.StStockHeader = Header;               
            }
            else if (btnAdd == "Save")
            {
                List<StStockHeaderModel> HeaderT = Model.StStockHeader;
                if (HeaderT.Count > 0)
                {
                    ClsStStockHeader lsstock = new ClsStStockHeader();
                    foreach (StStockHeaderModel tRow in HeaderT)
                    {
                        tRow.created_by = User.Identity.Name;
                        tRow.created_date = DateTime.Now;
                        tRow.last_edited_by = User.Identity.Name;
                        tRow.last_edited_date = DateTime.Now;

                        if (tRow.st_stock_header_id == 0)
                        {
                            if (tRow.material_id != 0)
                            {
                                tRow.Mode = 1;
                                lsstock.InsertUpdate(tRow);
                            }
                        }
                    }
                    ViewBag.Message = "Record save Sucessfully ";
                }
                mymodel.StStockHeader = HeaderT;
            }
            return View(mymodel);
        }

      
        public ActionResult Edit(int id)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.MaterialList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + " "), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");                       

            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            StStockHeaderModel StockHeader = new StStockHeaderModel();
            ClsStStockHeader lsStockHeader = new ClsStStockHeader();
            
            mymodel.StStockHeader = lsStockHeader.Stockheader(id);
            mymodel.StStockHeaderSearch = lsStockHeader.StockHeader(id);            

            
            if (mymodel.StStockHeaderSearch.material_id != 0)
            {                
                ViewBag.MaterialList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + SiteId + " "), "material_id", "material_name", mymodel.StStockHeaderSearch.material_id);
            }
            if (mymodel.StStockHeaderSearch.unit_code != 0)
            {
                ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc", mymodel.StStockHeaderSearch.unit_code);
            }
            if (mymodel.StStockHeaderSearch.site_id != 0)
            {
                ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name", mymodel.StStockHeaderSearch.site_id);
            }
            if (mymodel.StStockHeaderSearch.store_id != 0)
            {
                ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name", mymodel.StStockHeaderSearch.store_id);
            }
            if (mymodel.StStockHeaderSearch.rack_id != 0)
            {
                ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name", mymodel.StStockHeaderSearch.rack_id);
            }
                         
            Session["StockPosting"] = mymodel.StStockHeader;            
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Edit(StStockHeader_StockPostingModel Header, FormCollection form, string btnAdd )
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");
                       
            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            ClsStStockHeader lsStockHeader = new ClsStStockHeader();            

            if (btnAdd == "Save")
            {
                List<StStockHeaderModel> HeaderT = Header.StStockHeader;
                StStockHeaderModel stStock = Header.StStockHeaderSearch;
               
                 ClsStStockHeader lsstock = new ClsStStockHeader();
                if (HeaderT.Count > 0)
                { 
                    foreach (StStockHeaderModel tRow in HeaderT)
                    {                      
                      tRow.last_edited_by = User.Identity.Name;
                      tRow.last_edited_date = DateTime.Now;  
                        if (tRow.st_stock_header_id != 0)
                        {
                           tRow.Mode = 2;
                           lsstock.InsertUpdate(tRow);
                        } 
                    }
                }
                    ViewBag.Message = "Record save Sucessfully ";
                
                mymodel.StStockHeader = HeaderT;
            }
            return View("Edit", mymodel);
        }

        public ActionResult BulkEdit()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");

            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            ClsStStockHeader lsstock = new ClsStStockHeader();
            mymodel.StStockHeader = lsstock.StStockHeader("st_stock_header.site_id="+ SiteId + " ");
            return View(mymodel);

        }

        [HttpPost]
        public ActionResult BulkEdit(StStockHeader_StockPostingModel SSH, FormCollection form, string btnAdd)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");
            ClsStStockHeader lsStockHeader = new ClsStStockHeader();
            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            string lsFilter = string.Empty;

            if (btnAdd == "Add")
            {
                ModelState.Clear();
                if (SSH.StStockHeaderSearch.SiteId != null)
                {
                    lsFilter = lsFilter + "st_stock_header.site_id = " + SSH.StStockHeaderSearch.SiteId + " and ";
                }
                if (SSH.StStockHeaderSearch.MaterialId != null)
                {
                    lsFilter = lsFilter + "st_stock_header.material_id = " + SSH.StStockHeaderSearch.MaterialId + " and ";
                }
                if (SSH.StStockHeaderSearch.StoreId != null)
                {
                    lsFilter = lsFilter + "st_stock_header.store_id = " + SSH.StStockHeaderSearch.StoreId + " and ";
                }
                if (SSH.StStockHeaderSearch.RackId != null)
                {
                    lsFilter = lsFilter + "st_stock_header.rack_id = " + SSH.StStockHeaderSearch.RackId + " and ";
                }

                lsFilter = lsFilter + "st_stock_header.st_stock_header_id is not null ";
               
                mymodel.StStockHeader = lsStockHeader.StStockHeader(lsFilter);
                mymodel.StStockHeaderSearch = SSH.StStockHeaderSearch;
            }

            else if (btnAdd == "Save")
            {
                List<StStockHeaderModel> HeaderT = SSH.StStockHeader;
                StStockHeaderModel stStock = SSH.StStockHeaderSearch;
                ClsStStockHeader lsstock = new ClsStStockHeader();
                if (HeaderT.Count > 0)
                {
                    foreach (StStockHeaderModel tRow in HeaderT)
                    {
                        tRow.last_edited_by = User.Identity.Name;
                        tRow.last_edited_date = DateTime.Now;
                        if (tRow.st_stock_header_id != 0)
                        {
                            tRow.Mode = 2;
                            lsstock.InsertUpdate(tRow);
                        }
                    }
                }
                ViewBag.Message = "Record save Sucessfully ";
                mymodel.StStockHeader = HeaderT;
                mymodel.StStockHeaderSearch = SSH.StStockHeaderSearch;
            }
            return View(mymodel);
        }

         //------- Print -----------------------------------
        //public ActionResult ExportToExcel(StStockHeader_StockPostingModel SSH)
        //{
        //    int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
        //    string lsFilter = string.Empty;
        //    string FileName = string.Empty;

        //    FileName = "StockPosting";

        //    ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
        //    ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");
        //    ClsUnitMaster lsUnit = new ClsUnitMaster();
        //    ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "short_desc");
        //    ClsBrandMaster lsBM = new ClsBrandMaster();
        //    ViewBag.BrandList = new SelectList(lsBM.BrandMaster(), "brand_id", "brand_name");
        //    ClsSiteMaster lsSite = new ClsSiteMaster();
        //    ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
        //    ClsRackMaster lsRM = new ClsRackMaster();
        //    ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");

        //    if (SSH.StStockHeaderSearch.SiteId != null)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.site_id = " + SSH.StStockHeaderSearch.SiteId + " and ";
        //    }
        //    if (SSH.StStockHeaderSearch.MaterialId != null)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.material_id = " + SSH.StStockHeaderSearch.MaterialId + " and ";
        //    }            
        //    if (SSH.StStockHeaderSearch.RackId != null)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.rack_id = " + SSH.StStockHeaderSearch.RackId + " and ";
        //    }
        //    //if (SSH.StStockHeaderSearch.brand_id != null)
        //    //{
        //    //    lsFilter = lsFilter + "st_stock_header.brand_id = " + SSH.StStockHeaderSearch.brand_id + " and ";
        //    //}
        //    //if (SSH.StStockHeaderSearch.unit_code != null)
        //    //{
        //    //    lsFilter = lsFilter + "st_stock_header.unit_code = " + SSH.StStockHeaderSearch.unit_code + " and ";
        //    //}
        //    lsFilter = lsFilter + "st_stock_header.brand_id is not null ";
        //    Session["lsFilter"] = lsFilter;
        //    ClsStStockHeader lsStockHeader = new ClsStStockHeader();
        //    DataTable dt = lsStockHeader.StStockHeader_ExportData(lsFilter);

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        dt.TableName = "TEST";
        //        wb.Worksheets.Add(dt);
        //        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        wb.Style.Font.Bold = true;

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename= " + FileName + ".xlsx");

        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
        //    mymodel.StStockHeader = lsStockHeader.StStockHeader(lsFilter);
        //    return RedirectToAction("Index", "StockPosting");
        //}


        //------------------------------------------
        public JsonResult FillBySiteId(string SiteId, string SiteName)
        {
            ClsRackMaster lsRM = new ClsRackMaster();
            List<RackMasterModel> RackMaster = new List<RackMasterModel>();
            RackMaster = lsRM.RackMaster_Storewise(SiteId);
            var result = new { List = new SelectList(RackMaster, "rack_id", "rack_name") };
            return Json(result);
        }

        
        public ActionResult UnitCode(string MaterialId, string MaterialName)
        {
            string UnitCode = "*";
            string ShortName = "*";
            if (MaterialId != null && !string.IsNullOrEmpty(MaterialId))
            {
                ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
                UnitCode = lsMaterial.UnitCode(Convert.ToInt32(MaterialId));

                ClsUnitMaster lsUnit = new ClsUnitMaster();
                ShortName = lsUnit.ShortDesc(Convert.ToInt32(UnitCode));
            }

            try
            {
                return Json(new
                {
                    msg = ShortName
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult FillByUnitCode(string ItemCode, string ItemName)
        {
            //int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            //ClsStStockHeader stockHeader = new ClsStStockHeader();
            //var stock_qty = stockHeader.stStockQty(Convert.ToInt32(ItemCode), Convert.ToInt32(SiteId));
            var stock_qty = 0;
            ClsUnitMaster lsUom = new ClsUnitMaster();
            var result = new { List = new SelectList(lsUom.GetUOMMaster(Convert.ToInt32(ItemCode)), "unit_code", "short_desc"), ItemStockQty = stock_qty };
            return Json(result);
        }

    }
}