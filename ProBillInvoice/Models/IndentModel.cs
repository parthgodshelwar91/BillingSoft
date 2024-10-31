using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice
{
    public class IndentModel
    {        
        public IndentHeaderModel IndentHeaderSearch { get; set; }
        public List<IndentHeaderModel> IndentHeader { get; set; }
        public List<IndentDetailModel> IndentDetail { get; set; }

        public Ad_CompanyMasterModel Company { get; set; }

        public softwareReportSettingModel ReportSetting { get; set; }
        public TermsConditionMasterModel TermsCondition { get; set; }
    }
}