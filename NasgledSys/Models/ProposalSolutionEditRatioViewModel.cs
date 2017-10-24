using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProposalSolutionEditRatioViewModel
    {
        public Guid ProductKey { get; set; }
        public Guid ProposalKey { get; set; }
        public string ProfileProductNameText { get; set; }

        public int? ForEveryMainQty { get; set; }
        public int? ReplaceQty { get; set; }

    }
}