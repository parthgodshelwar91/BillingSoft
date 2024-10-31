using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;

namespace ProBillInvoice
{
    public class RCModel
    {
        public RCHeaderModel RCHeaderSearch { get; set; }
        public List<RCHeaderModel> RCHeader { get; set; }
        public List<RCDetailModel> RCDetail { get; set; }
        public PartyMasterModel PartyMasterSearch { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }
        public StateMasterModel StateMst { get; set; }
    }
}