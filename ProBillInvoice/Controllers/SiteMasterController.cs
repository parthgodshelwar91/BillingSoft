using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class SiteMasterController : Controller
    {
        // GET: SiteMaster
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            SiteMasterModel Site = new SiteMasterModel();
            ClsSiteMaster lsSite = new ClsSiteMaster();
            Site.site_id = lsSite.NextId();
            Site.SiteList = lsSite.SiteMaster(0);
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");
            return View(Site);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Titlename = "Edit";
            ClsSiteMaster lsSite = new ClsSiteMaster();
            SiteMasterModel Slite = lsSite.Sitemaster(id);
            Slite.SiteList = lsSite.SiteMaster(0);
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");
            return View("Create", Slite);
        }

        public ActionResult Details(int id)
        {
            ClsSiteMaster lsSite = new ClsSiteMaster();
            SiteMasterModel Slite = lsSite.Sitemaster(Convert.ToInt32(id));
            return View(Slite);
        }

        [HttpPost]
        public ActionResult Create(SiteMasterModel SM)
        {
            ClsSiteMaster lsSite = new ClsSiteMaster();
            if (SM.site_id != 0)
            {
                
                SM.last_edited_by = User.Identity.Name;
                SM.last_edited_date = DateTime.Now;

                lsSite.InsertUpdate(1,SM);
                ViewBag.Message = "Detail Save Sucessfully";
            }
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");
            SM.SiteList = lsSite.SiteMaster(0);
            ViewBag.Titlename = "Create";
            return View(SM);
        }        

        [HttpPost]
        public ActionResult Edit(SiteMasterModel SM)
        {
            ClsSiteMaster lsSite = new ClsSiteMaster();
            if (SM.site_id != 0)
            {
                SM.last_edited_by = User.Identity.Name;
                SM.last_edited_date = DateTime.Now;

                lsSite.InsertUpdate(2,SM);
                ViewBag.Message = "Detail Save Sucessfully";
            }
            SM.SiteList = lsSite.SiteMaster(0);
            ViewBag.Titlename = "Create";
            ClsCompanyMaster CM = new ClsCompanyMaster();
            ViewBag.CompanyList = new SelectList(CM.CompanyMaster(), "company_id", "company_name");
            return View("Create",SM);
        }        
    }
}