using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice
{
    public class PurchaseOrderModel
    {
        public PurchaseHeaderModel PurchaseHeaderSearch { get; set; }    
        public List<PurchaseHeaderModel> PurchaseHeader { get; set; }       
        public List<PurchaseDetailModel> PurchaseDetail { get; set; }
        public List<PurchaseTaxModel> PurchaseTax { get; set; }
        public PartyMasterModel PartyMasterSearch { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }
        public StateMasterModel StateMst { get; set; }
        public TermsConditionMasterModel TermsCondition { get; set; }
        public softwareReportSettingModel ReportSetting { get; set; }        
    }
}