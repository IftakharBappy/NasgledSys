using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProposalReportViewModel
    {
        public List<ReportTemplateModel> FromReportTemplateList { get; set; }
        public List<ReportTemplateModel> ToReportTemplateList { get; set; }

        public System.Guid ProposalKey { get; set; }
        public System.Guid CompanyKey { get; set; }
        public string CompanyName { get; set; }
    }
}