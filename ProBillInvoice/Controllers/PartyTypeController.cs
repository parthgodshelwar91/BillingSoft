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
    public class PartyTypeController : Controller
    {
        // GET: PartyType
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            PartyTypeModel Party = new PartyTypeModel();
            ClsPartyType lsParty = new ClsPartyType();           
            Party.type_id = lsParty.NextId();
            Party.PartyTypeList = lsParty.PartyType();
            return View(Party);
        }

        [HttpPost]
        public ActionResult Create(PartyTypeModel PT)
        {
            ClsPartyType lsParty = new ClsPartyType();          
            if (PT.type_id != 0)
            {
                lsParty.Insert(PT);
              ViewBag.Message = "Detail Save Sucessfully";
            }
            PT.PartyTypeList = lsParty.PartyType();
            ViewBag.Titlename = "Create";
            return View(PT);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Titlename = "Edit";
            ClsPartyType lsParty = new ClsPartyType();
            PartyTypeModel Party = lsParty.PartyType(id);
            Party.PartyTypeList = lsParty.PartyType();
            return View("Create", Party);
        }

        [HttpPost]
        public ActionResult Edit(PartyTypeModel PT)
        {
            ClsPartyType lsParty = new ClsPartyType();
            if (PT.type_id != 0)
            {
                lsParty.Update(PT);
                ViewBag.Message = "Detail Save Sucessfully";
            }
            PT.PartyTypeList = lsParty.PartyType();
            ViewBag.Titlename = "Create";
            return View("Create", PT);
        }

        public ActionResult Details(int id)
        {
            ClsPartyType lsParty = new ClsPartyType();
            PartyTypeModel Party = lsParty.PartyType(Convert.ToInt32(id));
            return View(Party);
        }
    }
}