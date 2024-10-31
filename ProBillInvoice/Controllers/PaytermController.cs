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
    public class PaytermController : Controller
    {
        string lsUsername = Convert.ToString(System.Web.HttpContext.Current.Session["LoginUserName"]);
        public ActionResult Index()
        {
            ClsPayterm lsPayterm = new ClsPayterm();
            return View(lsPayterm.Payterm());
        }

        public ActionResult Create()
        {
            PaytermModel Payterm = new PaytermModel();
            ClsPayterm lsPayterm = new ClsPayterm();
            Payterm.payterm_code = lsPayterm.NextId();
            Payterm.PaytermList = lsPayterm.Payterm();
            ViewBag.Titlename = "Create";
            return View(Payterm);
        }
        public ActionResult Edit(int id)
        {
            ClsPayterm lsPayterm = new ClsPayterm();
            PaytermModel Payterm = lsPayterm.Payterm(id);
            Payterm.PaytermList = lsPayterm.Payterm();
            return View(Payterm);
        }

        public ActionResult Details(int id)
        {
            ClsPayterm lsPayterm = new ClsPayterm();
            PaytermModel Payterm = lsPayterm.Payterm(id);
            return View(Payterm);
        }

        [HttpPost]
        public ActionResult Create(PaytermModel payterm)
        {
            ClsPayterm lsPayterm = new ClsPayterm();
            if (payterm.payterm_code != 0)
            {
               //int payterm_code = payterm.payterm_code;
                string payterm_desc = payterm.payterm_desc;
                decimal payterm_days = payterm.payterm_days;
                bool defunct = payterm.defunct;               
                string last_edited_by = lsUsername;
                DateTime last_edited_date = DateTime.Now;

                //InsertTable
                lsPayterm.InsertUpdate(1, payterm.payterm_code, payterm_desc, payterm_days, defunct, last_edited_by, last_edited_date);
                ViewBag.Message = "Detail save successfully";
            }
            payterm.PaytermList = lsPayterm.Payterm();
            return View(payterm);
        }

        [HttpPost]
        public ActionResult Edit(PaytermModel payterm)
        {
            ClsPayterm lsPayterm = new ClsPayterm();
            if (payterm.payterm_code != 0)
            {
                int payterm_code = payterm.payterm_code;
                string payterm_desc = payterm.payterm_desc;
                decimal payterm_days = payterm.payterm_days;
                bool defunct = payterm.defunct;
                string last_edited_by = lsUsername;
                DateTime last_edited_date = DateTime.Now;
                //Update Table
                lsPayterm.InsertUpdate(2,payterm_code, payterm_desc, payterm_days, defunct, last_edited_by, last_edited_date);
                ViewBag.Message = "Detail save successfully";
            }
            payterm.PaytermList = lsPayterm.Payterm();
            return View(payterm);
        }

    }
}