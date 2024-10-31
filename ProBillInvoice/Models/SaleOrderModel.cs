using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class SaleOrderModel
    {
        public SaleOrderHeaderModel SaleOrderHeaderSearch { get; set; }
        public List<SaleOrderHeaderModel> SaleOrderHeader { get; set; }
        public List<SaleOrderDetailModel> SaleOrderDetail { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }
        public PartyMasterModel partymaster { get; set; }
        public CityMasterModel Citymaster { get; set; }
        public StateMasterModel StateMaster { get; set; }
        public TermsConditionMasterModel TermsCondition { get; set; }
        public softwareReportSettingModel ReportSetting { get; set; }
    }
}