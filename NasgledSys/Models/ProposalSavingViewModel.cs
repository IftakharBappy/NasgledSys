using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProposalSavingViewModel
    {
        public Guid? ProposalKey { get; set; }
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Nullable<decimal> AnnualCost { get; set; }
        public Nullable<decimal> AnnualEnergy { get; set; }
        public Nullable<decimal> ReduceCarbon { get; set; }
    }
}