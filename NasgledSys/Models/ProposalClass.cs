using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProposalListClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? ProposalKey { get; set; }
        [Display(Name = "Proposal Name*...")]
        [Required(ErrorMessage = "Name is required")]
        public string ProposalName { get; set; }

        public decimal? TotalPrice { get; set; }
        public decimal? Incentives { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? AnnualSaving { get; set; }
        public decimal? ROI { get; set; }
     
    }
        public class ProposalClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? ProposalKey { get; set; }
        [Display(Name = "Proposal Name*...")]
        [Required(ErrorMessage = "Name is required")]
        public string ProposalName { get; set; }
       
        public Guid? PreparedBy { get; set; }
        public Guid? CreateDate { get; set; }
        public byte[] Pic { get; set; }
        public string PicType { get; set; }
        public string Picname { get; set; }
        public string PageInfo { get; set; }
        public string IntroductionText { get; set; }
        public string Team { get; set; }
        public string Legal { get; set; }
        public string Disclaimer { get; set; }
        public string Reference { get; set; }
        public string Billing { get; set; }
        public string Notes { get; set; }
        public string Summary { get; set; }
        public string Estimal { get; set; }
        public string ProjectDescription { get; set; }
    }
}