using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice
{
    public class GrinModel
    {
        public GrinHeaderModel GrinHeaderSearch { get; set; }
        public List<GrinHeaderModel> GrinHeader { get; set; }
        public List<GrinDetailModel> GrinDetail { get; set; }
        public List<GrinTaxModel> GrinTax { get; set; }
        public PartyMasterModel partymaster { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }
        public StateMasterModel StateMst { get; set; }
        public TermsConditionMasterModel TermsCondition { get; set; }
        public softwareReportSettingModel ReportSetting { get; set; }
    }
}