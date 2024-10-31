using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleInvoiceModel
    {
        public List<SaleInvoiceHeaderModel> SaleInvoiceList { get; set; }
        public PartyMasterModel partyMasterSearch { get; set; }
        public List<SaleInvoiceDetailModel> InvoiceDetails { get; set; }
        public SaleInvoiceHeaderModel InvoiceHeaderSearch { get; set; }
        public List<SaleInvoiceTaxesModel> InvoiceTax { get; set; }
        public SaleInvoiceDetailModel SaleInvoice { get; set; }
        public List<SaleInvoiceHeaderModel> InvoiceHeader { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }
        public PartyMasterModel partymaster { get; set; }
        public StateMasterModel StateMst { get; set; }
        public TermsConditionMasterModel TermsCondition {get;set;}

        public List<TicketsModel> TicketsList { get; set; }  
    }
}