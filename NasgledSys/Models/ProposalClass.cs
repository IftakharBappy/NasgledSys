using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class SummaryClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        public decimal? TotalProjectSaving { get; set; }
        public decimal? AnnualCostSaving { get; set; }
        public decimal? AnnualEnergySaving { get; set; }
        public decimal? ProductCosts { get; set; }
        public decimal? LaborCosts { get; set; }
        public decimal? ShippingCosts { get; set; }
        public decimal? MiscProductCost { get; set; }
        public decimal? EstimatedSalesTax { get; set; }
        public decimal? CostOfGoodsSold { get; set; }
        public decimal? NetSavings { get; set; }
        public decimal? SimplePaybackYears { get; set; }
        public decimal? SimpleROI { get; set; }
        public decimal? GrossMargin { get; set; }
        public decimal? GrossMarginPercentage { get; set; }
        public decimal? TotalProjectPrice { get; set; }
        public decimal? EstimatedIncentiveRebate { get; set; }
        public decimal? NetProjectPrice { get; set; }
        public decimal? CostOfWaitingOneMonth { get; set; }
        public decimal? CostOfWaitingOneYear { get; set; }
        public decimal? CostOfWaitingFiveYear { get; set; }
        [Display(Name = "Product Markup Percentage")]
        public decimal? MarkupPercentage { get; set; }
        [Display(Name = "Labor Cost")]
        public decimal? LaborCost { get; set; }
        [Display(Name = "Shipping Cost")]
        public decimal? ShippingCost { get; set; }
        [Display(Name = "Misc Cost")]
        public decimal? MiscCost { get; set; }
        [Display(Name = "179D Tax Incentive")]
        public decimal? TaxIncentives { get; set; }
        [Display(Name = "Installation Labor Charge")]
        public decimal? InstallationLaborCharge { get; set; }

    }

    public class FinanceClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        [Display(Name = "Percentage Of Project Cost Financed")]
        [Required(ErrorMessage = "Percentage Of Project Cost Financed is required.")]
        public Nullable<decimal> PercentageOfProjectCost { get; set; }
        [Display(Name = "Financing Interest Rate")]
        [Required(ErrorMessage = "Financing Interest Rate is required.")]
        public Nullable<decimal> FinancingInterestRate { get; set; }
        [Display(Name = "Financing Loan Month")]
        [Required(ErrorMessage = "Financing Loan Month is required.")]
        public Nullable<decimal> LoanMonth { get; set; }
        [Display(Name = "Principle")]
        public Nullable<decimal> Principle { get; set; }
        [Display(Name = "Interest Rate")]
        public Nullable<decimal> InterestRate { get; set; }
        [Display(Name = "Loan Months")]
        public Nullable<decimal> DisplayLoanMonth { get; set; }
        [Display(Name = "Monthly Payments")]
        public Nullable<decimal> MonthlyPayment { get; set; }
        [Display(Name = "Total Payments")]
        public Nullable<decimal> TotalPayment { get; set; }
        [Display(Name = "Cost Savings")]
        public Nullable<decimal> CostSavings { get; set; }
        [Display(Name = "-Loan Payment")]
        public Nullable<decimal> LoanPayment { get; set; }
        [Display(Name = "MOnthly Cash Flow")]
        public Nullable<decimal> MonthlyCashFlow { get; set; }
    }
    public class NoteClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        [Display(Name = "Internal Notes")]
        [Required(ErrorMessage = "Please include some Notes if you want to save any Information.")]
        public string Notes { get; set; }
    }
    public class ProposalListClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? ProposalKey { get; set; }
        [Display(Name = "Proposal Name*...")]
        [Required(ErrorMessage = "Name is required")]
        public string ProposalName { get; set; }
        [Display(Name = "Prepared By User*...")]
        [Required(ErrorMessage = "Prepared By User is required")]
        public string PreparedByUser { get; set; }

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