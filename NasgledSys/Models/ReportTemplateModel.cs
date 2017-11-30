using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ReportTemplateModel
    {
        public System.Guid? TemplateKey { get; set; }
        public string FactorName { get; set; }
        public Nullable<int> TLevel { get; set; }
    }
}