using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProposalTemplateModel
    {
        public System.Guid ReportKey { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        public Nullable<System.Guid> ProposalKey { get; set; }
        public string FactorName { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
       
    }
}