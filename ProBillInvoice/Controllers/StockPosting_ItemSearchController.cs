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
    public class StockPosting_ItemSearchController : Controller
    {
        // GET: StockPosting_ItemSearch
        public ActionResult Index()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMaterial.MaterialMaster(), "material_id", "material_name");            
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            ViewBag.RackList = new SelectList(lsRM.RackMaster(), "rack_id", "rack_name");
            
            StStockHeader_StockPostingModel mymodel = new StStockHeader_StockPostingModel();
            StStockHeaderModel HeaderSearch = new StStockHeaderModel();
            StStockHeaderModel Header = new StStockHeaderModel();            
            Header.SiteId = SiteId;

            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            mymodel.StStockHeader = lsStStockHeader.StockHeader_ItemSearch("st_stock_header.site_id = " + SiteId + " ");
            mymodel.StStockHeaderSearch = Header;
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Search(StStockHeader_StockPostingModel WBT, string ItemNameSearch)
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
            if (WBT.StStockHeaderSearch.StoreId != null && WBT.StStockHeaderSearch.StoreId != 0)
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
            mymodel.StStockHeader = lsStStockHeader.StockHeader_ItemSearch(lsFilter);

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