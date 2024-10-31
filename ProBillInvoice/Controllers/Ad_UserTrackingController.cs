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
    public class Ad_UserTrackingController : Controller
    {
        // GET: UserTracking
        string currentMonth = DateTime.Now.Month.ToString();
        string currentYear = DateTime.Now.Year.ToString();
        public ActionResult Index()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0: yyyy-MM-dd 23:59:59.999}", DateTime.Now);
                        
            Ad_UserLoginTrackingModel mymodel = new Ad_UserLoginTrackingModel();
            ClsUserLoginTracking lsUserTracking = new ClsUserLoginTracking();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);

            mymodel.UserLoginTrackingList = lsUserTracking.UserTrackingList("log_in_time between '" + lsFromDate + "' and '" + lsToDate + "'");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Search(Ad_UserLoginTrackingModel UT)
        {
            string lsFromDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", UT.FromDate);
            string lsToDate = string.Format("{0: yyyy/MM/dd 23:59:59.999}", UT.ToDate);
                        
            string lsFilter = string.Empty;
            //if (SOM.SaleOrderHeaderSearch.PartyId != null)
            //{
            //    lsFilter = lsFilter + " sale_order_header. party_id = " + SOM.SaleOrderHeaderSearch.PartyId + " AND ";
            //}            
            lsFilter = lsFilter + "log_in_time between '" + lsFromDate + "' and '" + lsToDate + "' ";

            Ad_UserLoginTrackingModel mymodel = new Ad_UserLoginTrackingModel();
            ClsUserLoginTracking lsUserTracking = new ClsUserLoginTracking();
            mymodel.UserLoginTrackingList = lsUserTracking.UserTrackingList(lsFilter);
            return View("Index", mymodel);
        }
    }
}