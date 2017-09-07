using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ClientCompanyViewModel
    {
        public System.Guid ClientCompanyKey { get; set; }
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string CompanyName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Industry Type*")]
        [Required(ErrorMessage = "Industry Type is required")]
        public Nullable<System.Guid> IndustryTypeKey { get; set; }
        public Nullable<System.Guid> ProfileKey { get; set; }
        [Display(Name = "Number Of Sales Person")]
        public Nullable<int> NoOfSalesPerson { get; set; }
        [Display(Name = "Office Phone*")]
        [Required(ErrorMessage = "Office Phone is required")]
        public string OfficePhone { get; set; }
        [Display(Name = "Address*")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Display(Name = "Zipcode*")]
        [Required(ErrorMessage = "Zipcode is required")]
        public string Zipcode { get; set; }
        [Display(Name = "Billing Contact Name")]
        public string BillingContactName { get; set; }
        [Display(Name = "Billing Contact EMail")]
        public string BillingContactEMail { get; set; }
        [Display(Name = "Billing Address Same As Office")]
        public bool IsAddressSameAsOffice { get; set; } = false;
        [Display(Name = "Proposal Intro")]
        public string ProposalIntro { get; set; }
        [Display(Name = "Proposal Team")]
        public string ProposalTeam { get; set; }
        [Display(Name = "Proposal Legal")]
        public string ProposalLegal { get; set; }
        [Display(Name = "Proposal Disclaimer")]
        public string ProposalDisclaimer { get; set; }
        [Display(Name = "Proposal Reference")]
        public string ProposalReference { get; set; }
        [Display(Name = "Proposal Footer")]
        public string EstimateFooter { get; set; }
        [Display(Name = "Markup/Margin Type")]
        public string MarkupOrMargin { get; set; }
        [Display(Name = "Markup/Margin Percent")]
        public Nullable<decimal> MarkupOrMarginPercentage { get; set; }
        [Display(Name = "City*")]
        [Required(ErrorMessage = "City is required")]
        public Nullable<int> CityKey { get; set; }
        [Display(Name = "State*")]
        [Required(ErrorMessage = "State is required")]
        public Nullable<int> StateKey { get; set; }
        
        public List<ClientCompanyViewModel> ClientCompanyViewModelList { get; set; }
    }
}