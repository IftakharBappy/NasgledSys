using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NasgledSys.Models
{
    public class ProposalToCustomizeViewModel
    {
        public System.Guid ProposalKey { get; set; }

        [Display(Name = "Proposal Name")]
        public string ProposalName { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        public Nullable<System.Guid> CompanyKey { get; set; }//CompanyName
        public string CompanyName { get; set; }


        [Display(Name = "Intro")]
        public string IntroductionText { get; set; }
        public string Team { get; set; }
        public string Legal { get; set; }
        public string Disclaimer { get; set; }
        public string Reference { get; set; }

        [Display(Name = "Billing Notes")]
        public string Billing { get; set; }

        [Display(Name = "Summury Estimate Project Description")]
        public string Summary { get; set; }

        [Display(Name = "Prepared By User")]
        public string PreparedByText { get; set; }

        [Display(Name = "Cover Photo")]
        public byte[] Pic { get; set; }
        public string PicType { get; set; }
        public string Picname { get; set; }
    }
}