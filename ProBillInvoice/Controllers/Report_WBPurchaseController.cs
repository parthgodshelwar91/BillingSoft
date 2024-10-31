using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;

namespace ProBillInvoice.Controllers
{
    //[Authorize]
    public class Report_WBPurchaseController : Controller
    {
        // GET: Report_WBPurchase
        string currentMonth = DateTime.Now.Month.ToString();
        string currentYear = DateTime.Now.Year.ToString();

        //***** Ticket Incoming Report ********************************************************        
        public ActionResult TicketIncoming()
        {            
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            TicketsModel mymodel = new TicketsModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Incoming Material Report";

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");

            ClsTickets lsTicket = new ClsTickets();            
            mymodel.TicketsList = lsTicket.TicketsList("tickets.acct_type = 'P' and tickets.pending = 'False' and tickets.closed = 'True' and tickets.ticket_date_time between '" + lsFromDate + "' and '" + lsToDate + "' ");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult TicketIncoming(TicketsModel Ticket)
        {
            string lsFilter = string.Empty;
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.ToDate);
            ViewBag.Titlename = "Incoming Material Report";

            TicketsModel mymodel = new TicketsModel();
            ClsTickets lsTicket = new ClsTickets();

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name", Ticket.PartyId);

            if (Ticket.PartyId != null)
            {
               lsFilter = lsFilter + "tickets.party_id = " + Ticket.PartyId + " and ";
            }
            lsFilter = lsFilter + "tickets.acct_type = 'P' and tickets.pending = 'False' and tickets.closed = 'True' and tickets.ticket_date_time between '" + lsFromDate + "' and '" + lsToDate + "' ";

            mymodel.TicketsList = lsTicket.TicketsList(lsFilter);
            return View(mymodel);
        }


        //***** Ticket Purchase Summary Partywise Report **************************************
        public ActionResult TicketPurchaseSummary_Partywise()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_TicketsModel mymodel = new Temp_TicketsModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Ticket Purchase Summary Partywise";

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name");

            ClsTickets_Reports lsTicket = new ClsTickets_Reports();
            lsTicket.spTicketPurchaseSummary_Partywise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));         
            mymodel.Temp_TicketsList = lsTicket.Temp_TicketsList("party_id IS NOT NULL");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult TicketPurchaseSummary_Partywise(Temp_TicketsModel Ticket)
        {
            string lsFilter = string.Empty;
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.ToDate);
            ViewBag.Titlename = "Ticket Purchase Summary Partywise";

            Temp_TicketsModel mymodel = new Temp_TicketsModel();
            ClsTickets_Reports lsTicket = new ClsTickets_Reports();
            if (Ticket.PartyId != null)
            {
                lsFilter = lsFilter + "party_id = " + Ticket.PartyId + " and ";
            }
            lsFilter = lsFilter + "party_id IS NOT NULL ";

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("S"), "party_id", "party_name", Ticket.PartyId);

            lsTicket.spTicketPurchaseSummary_Partywise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_TicketsList = lsTicket.Temp_TicketsList(lsFilter);
            return View(mymodel);
        }


        //***** Ticket Purchase Summary Materialwise Report ***********************************
        public ActionResult TicketPurchaseSummary_Materialwise()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_TicketsModel mymodel = new Temp_TicketsModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Ticket Purchase Summary Materialwise";

            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMM.MaterialMaster_Type("Purchase"), "material_id", "material_name");

            ClsTickets_Reports lsTicket = new ClsTickets_Reports();
            lsTicket.spTicketPurchaseSummary_Partywise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_TicketsList = lsTicket.Temp_TicketsList_Materialwise("material_id IS NOT NULL");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult TicketPurchaseSummary_Materialwise(Temp_TicketsModel Ticket)
        {
            string lsFilter = string.Empty;
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.ToDate);
            ViewBag.Titlename = "Ticket Purchase Summary Materialwise";

            Temp_TicketsModel mymodel = new Temp_TicketsModel();
            ClsTickets_Reports lsTicket = new ClsTickets_Reports();
            if (Ticket.MaterialId != null)
            {
                lsFilter = lsFilter + "material_id = " + Ticket.MaterialId + " and ";
            }
            lsFilter = lsFilter + "material_id IS NOT NULL ";

            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMM.MaterialMaster_Type("Purchase"), "material_id", "material_name", Ticket.MaterialId);

            lsTicket.spTicketPurchaseSummary_Partywise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_TicketsList = lsTicket.Temp_TicketsList_Materialwise(lsFilter);
            return View(mymodel);
        }


        //***** Ticket Purchase Summary Tansporterwise Report ********************************
        public ActionResult TicketPurchaseSummary_Transporterwise()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_TicketsModel mymodel = new Temp_TicketsModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Purchase Summary Transporterwise";

            ClsTransporterMaster lsMM = new ClsTransporterMaster();
            ViewBag.TransporterList = new SelectList(lsMM.Transporter(), "transporter_id", "transporter_name");

            ClsTickets_Reports lsTicket = new ClsTickets_Reports();
            lsTicket.spTicketPurchaseSummary_Transporterwise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_TicketsList = lsTicket.Temp_TicketsList_Transporterwise("transporter_id IS NOT NULL");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult TicketPurchaseSummary_Transporterwise(Temp_TicketsModel Ticket)
        {
            string lsFilter = string.Empty;
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.ToDate);
            ViewBag.Titlename = "Purchase Summary Transporterwise";

            Temp_TicketsModel mymodel = new Temp_TicketsModel();
            ClsTickets_Reports lsTicket = new ClsTickets_Reports();
            if (Ticket.TransporterId != null)
            {
                lsFilter = lsFilter + "transporter_id = " + Ticket.TransporterId + " and ";
            }
            lsFilter = lsFilter + "transporter_id IS NOT NULL ";

            ClsTransporterMaster lsMM = new ClsTransporterMaster();
            ViewBag.TransporterList = new SelectList(lsMM.Transporter(), "transporter_id", "transporter_name", Ticket.TransporterId);

            lsTicket.spTicketPurchaseSummary_Transporterwise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_TicketsList = lsTicket.Temp_TicketsList_Transporterwise(lsFilter);
            return View(mymodel);
        }


        //***** Ticket Daily Transaction - Purchase Report ********************************
        public ActionResult TicketDailyTransaction_Purchase()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            TicketsModel mymodel = new TicketsModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Ticket Daily Transaction - Purchase";                       

            ClsTickets_Reports lsTicket = new ClsTickets_Reports();            
            mymodel.TicketsList = lsTicket.TicketsList("tickets.acct_type = 'P' and tickets.pending = 'False' and tickets.closed = 'True' and tickets.ticket_date_time between '" + lsFromDate + "' and '" + lsToDate + "' ");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult TicketDailyTransaction_Purchase(Temp_TicketsModel Ticket)
        {
            string lsFilter = string.Empty;
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Ticket.ToDate);
            ViewBag.Titlename = "Ticket Daily Transaction - Purchase";

            TicketsModel mymodel = new TicketsModel();
            ClsTickets_Reports lsTicket = new ClsTickets_Reports();            
            lsFilter = lsFilter + "tickets.acct_type = 'P' and tickets.pending = 'False' and tickets.closed = 'True' and tickets.ticket_date_time between '" + lsFromDate + "' and '" + lsToDate + "' ";
                            
            mymodel.TicketsList = lsTicket.TicketsList(lsFilter);
            return View(mymodel);
        }
    }
}