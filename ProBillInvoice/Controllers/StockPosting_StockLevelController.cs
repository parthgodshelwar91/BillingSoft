using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ClosedXML.Excel;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class StockPosting_StockLevelController : Controller
    {
        // GET: StockPosting_StockLevel
        public ActionResult Index()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");
            ClsBrandMaster lsBM = new ClsBrandMaster();
            ViewBag.BrandList = new SelectList(lsBM.BrandMaster(), "brand_id", "brand_name");
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");

            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");

            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            StStockHeaderModel HeaderSearch = new StStockHeaderModel();
            StStockHeaderModel Header = new StStockHeaderModel();
            Header.SiteId = SiteId;
            Header.Storewise_flag = true;

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            mymodel.StStockHeader = lsStStockHeader.StockHeader_BrandWiseLevel("st_stock_header.site_id = 0 ");
            mymodel.StStockHeaderSearch = Header;
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Search(StStockHeader_StockPostingModel WBT, string ItemCodeSearch, string ItemNameSearch)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFilter = string.Empty;
            if (WBT.StStockHeaderSearch.MaterialId != null)
            {
                lsFilter = lsFilter + "st_stock_header.material_id = " + WBT.StStockHeaderSearch.MaterialId + " and ";
            }              
            if (WBT.StStockHeaderSearch.SiteId != null)
            {
                lsFilter = lsFilter + "st_stock_header.site_id = " + WBT.StStockHeaderSearch.SiteId + " and ";
            }
            if (WBT.StStockHeaderSearch.StoreId != null)
            {
                lsFilter = lsFilter + "st_stock_header.store_id = " + WBT.StStockHeaderSearch.StoreId + " and ";
            }
            if (WBT.StStockHeaderSearch.RackId != null && WBT.StStockHeaderSearch.RackId != 0)
            {
                lsFilter = lsFilter + "st_stock_header.rack_id = " + WBT.StStockHeaderSearch.RackId + " and ";
            }
            if (ItemNameSearch != null && !string.IsNullOrEmpty(ItemNameSearch))
            {
                lsFilter = lsFilter + "material_mst.material_name like '%" + ItemNameSearch + "%' and ";
            }

            lsFilter = lsFilter + "st_stock_header.material_id is not null ";
            Session["lsFilter"] = lsFilter;

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            //mymodel.StStockHeader = lsStStockHeader.StockHeader_BrandWiseLevel(lsFilter);

            bool cbBrandWise = WBT.StStockHeaderSearch.BrandWise_flag;
            bool cbStorewise = WBT.StStockHeaderSearch.Storewise_flag;
            if (cbBrandWise == true && cbStorewise == false)
            {                
                mymodel.StStockHeader = lsStStockHeader.StockHeader_BrandWiseLevel(lsFilter);
            }
            else if (cbBrandWise == false && cbStorewise == true)
            {                
                mymodel.StStockHeader = lsStStockHeader.StockHeader_StoreWiseLevel(lsFilter);
            }
            else if ((cbBrandWise == false && cbStorewise == false) || (cbBrandWise == true  && cbStorewise == true ))
            {
                mymodel.StStockHeader = lsStStockHeader.StockHeader_BrandWiseLevel("st_stock_header.site_id = 0 ");                
            }
            
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");            
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");   
            return View("Index", mymodel);
        }

        //[HttpPost]
        //public ActionResult ExportToxcel(StStockHeader_StockPostingModel WBT, string ItemCodeSearch, string ItemNameSearch)
        //{
        //    string lsFilter = string.Empty;
        //    string FileName = string.Empty;
        //    FileName = "StockPosting StockLevel";

        //    if (WBT.StStockHeaderSearch.MaterialId != null)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.material_id = " + WBT.StStockHeaderSearch.MaterialId + " and ";
        //    }
        //    if (WBT.StStockHeaderSearch.SiteId != null)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.site_id = " + WBT.StStockHeaderSearch.SiteId + " and ";
        //    }
        //    if (WBT.StStockHeaderSearch.StoreId != null)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.store_id = " + WBT.StStockHeaderSearch.StoreId + " and ";
        //    }
        //    if (WBT.StStockHeaderSearch.RackId != null && WBT.StStockHeaderSearch.RackId != 0)
        //    {
        //        lsFilter = lsFilter + "st_stock_header.rack_id = " + WBT.StStockHeaderSearch.RackId + " and ";
        //    }
        //    if (ItemNameSearch != null && !string.IsNullOrEmpty(ItemNameSearch))
        //    {
        //        lsFilter = lsFilter + "material_mst.material_name like '%" + ItemNameSearch + "%' and ";
        //    }

        //    lsFilter = lsFilter + "st_stock_header.material_id is not null ";
        //    Session["lsFilter"] = lsFilter;

        //    StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
        //    ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
        //    DataTable dt = lsStStockHeader.StockPosting_ExportData(lsFilter);
                       
        //    bool cbBrandWise = WBT.StStockHeaderSearch.BrandWise_flag;
        //    bool cbStorewise = WBT.StStockHeaderSearch.Storewise_flag;
        //    if (cbBrandWise == true && cbStorewise == false)
        //    {
        //        mymodel.StStockHeader = lsStStockHeader.StockHeader_BrandWiseLevel(lsFilter);
        //    }
        //    else if (cbBrandWise == false && cbStorewise == true)
        //    {
        //        mymodel.StStockHeader = lsStStockHeader.StockHeader_StoreWiseLevel(lsFilter);
        //    }
        //    else if ((cbBrandWise == false && cbStorewise == false) || (cbBrandWise == true && cbStorewise == true))
        //    {
        //        mymodel.StStockHeader = lsStStockHeader.StockHeader_BrandWiseLevel("st_stock_header.site_id = 0 ");
        //    }
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
            
        //    return RedirectToAction("Index", "StockPosting");
        //}

        //------------------------------------------
        public JsonResult FillByStoreId(string StoreId, string StoreName)
        {
            ClsRackMaster lsRM = new ClsRackMaster();
            List<RackMasterModel> RackMaster = new List<RackMasterModel>();
            RackMaster = lsRM.RackMaster_Storewise(StoreId);
            var result = new { List = new SelectList(RackMaster, "rack_id", "rack_name") };
            return Json(result);
        }
    }
}