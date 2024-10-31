using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace ProBillInvoice.Controllers
{
    public class UtilsController : Controller
    {
        // GET: Utils
        public ActionResult Index()
        {
            return View();
        }
               
        public ActionResult CheckExists(string tablename, string colname, string checkvalue)
        {

            ClsDashboard CE = new ClsDashboard();
            string isexists = CE.CheckExists(tablename, colname, checkvalue);

            try
            {
                return Json(new
                {
                    CheckExistsData = isexists
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
        
}