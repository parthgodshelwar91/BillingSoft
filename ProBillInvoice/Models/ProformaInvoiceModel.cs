using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class ProformaInvoiceModel
    {
        public ProformaInvoiceHeaderModel ProformaInvHeader { get; set; }
        public List<ProformaInvoiceHeaderModel> ProformaHeaderList { get; set; }
        public ProformaInvoiceDetailModel ProformaInvDetail { get; set; }
        public List<ProformaInvoiceDetailModel> ProformaDetail { get; set; }
        public List<MaterialMasterModel> Material { get; set; }

        public Ad_CompanyMasterModel Company { get; set; }
        //public PartyMasterModel PartyMasterSearch { get; set; }
        public EnquiryDetailsModel EnquiryDetailsSearch { get; set; }
       

        public StateMasterModel StateMst { get; set; }
        public TermsConditionMasterModel TermsCondition { get; set; }

        public softwareReportSettingModel ReportSetting { get; set; }

        //public List<ProformaInvoiceDetailModel> ProformaDetail1 { get; set; }



    }
}