using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice
{
    public class CRM_SaleQuotationModel
    {
        public SaleQuotationHeaderModel SaleQuotationHeaderSearch { get; set; }
        public List<SaleQuotationHeaderModel> SaleQuotationHeader { get; set; }
        public List<SaleQuotationDetailModel> SaleQuotationDetail { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }
        //public PartyMasterModel PartyMasterSearch { get; set; }
        public EnquiryDetailsModel EnquiryDetailsSearch { get; set; }
        public StateMasterModel StateMst { get; set; }
        public TermsConditionMasterModel TermsCondition { get; set; }
        public softwareReportSettingModel ReportSetting { get; set; }
    }
}