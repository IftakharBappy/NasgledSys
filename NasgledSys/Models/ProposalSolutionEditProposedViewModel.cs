using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NasgledSys.Models
{
    public class ProposalSolutionEditProposedViewModel
    {
        public Guid ProductKey { get; set; }
        public Guid ProposalKey { get; set; }
        [Display(Name = "Existing")]
        public string ProfileProductNameText { get; set; }

        [Display(Name = "Proposed Product")]
        public Guid ProposedProfileProductKey { get; set; }
    }
}