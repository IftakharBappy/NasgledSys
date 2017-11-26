using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class SolutionMainListClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
       
      public List<AreaProductViewClass> AreaList { get; set; }

    }
    public class ChangeOperatingScheduleClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        public Guid? ProductKey { get; set; }
        [Display(Name = "Operating Schedule")]
        [Required(ErrorMessage = "Operating Schedule is required if you want to Save.")]
        public Guid? OperatingScheduleKey { get; set; }
       

    }
    public class ProductSolution
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        public Guid? ProductKey { get; set; }
        public string ExsistingName { get; set; }
        public List<ReuseSolutionClass> ExistingList { get; set; }
        [Display(Name = "Search for a product")]
        public Guid? ItemKey { get; set; }
        [Display(Name = "Pick a Product Category")]
        public Guid? CategoryKey { get; set; }
        [Display(Name = "Pick a Subcategory")]
        public Guid? SubcategoryKey { get; set; }
        [Display(Name = "Select a Manufacture")]
        public Guid? ManufacturerKey { get; set; }
        [Display(Name = "Select a Catelogue")]
        public Guid? CatelogueKey { get; set; }
        [Display(Name = "Pick a Product Type")]
        public Guid? ProduictTypeKey { get; set; }
        [Display(Name = "for every")]
        public int? forEvery { get; set; }
        [Display(Name = "replace with")]
        public int? replaceWith { get; set; }
       
        public string newItem { get; set; }

    }
    public class ProposalProductDetailClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        public Guid? ProductKey { get; set; }
        [Display(Name = "Appy To")]
        public string ApplyTo { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public Nullable<System.Guid> FixtureKey { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Solution Name is required.")]
        public string SolutionName { get; set; }
        [Display(Name = "Incentive Type")]
        public Nullable<System.Guid> IncentiveTypeKey { get; set; }
        [Display(Name = "Incentive Amount")]
        public Nullable<decimal> IncentiveAmount { get; set; }
        [Display(Name = "Incentive MAX Type")]
        public Nullable<System.Guid> IncentiveMaxTypeKey { get; set; }
        [Display(Name = "Incentive MAX Amount")]
        public Nullable<decimal> IncentiveMaxAmount { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }
        [Display(Name = "Product Model No")]
        [Required(ErrorMessage = "Product Model No is required.")]
        public string ModelNo { get; set; }
        [Display(Name = "Watt Per Product")]
        [Required(ErrorMessage = "Watt Per Product is required.")]
        public Nullable<decimal> WattPerProduct { get; set; }
        [Display(Name = "L70")]
        [Required(ErrorMessage = "L70 is required.")]
        public Nullable<decimal> L70 { get; set; }
        [Display(Name = "Thermal Efficiency")]
        public Nullable<decimal> ThermalEfficency { get; set; }
        [Display(Name = "Custom Product Notes")]
        public string CustomNotes { get; set; }
        [Display(Name = "Product Cost")]
        public Nullable<decimal> ProductCost { get; set; }
        [Display(Name = "Markup Percentage")]
        public Nullable<decimal> MarkupPercentage { get; set; }
        [Display(Name = "Installation Time")]
        public Nullable<decimal> InstallationTime { get; set; }
        [Display(Name = "Installation Cost")]
        public Nullable<decimal> InstallationCost { get; set; }
        [Display(Name = "Shiping Cost")]
        public Nullable<decimal> ShipingCost { get; set; }
        [Display(Name = "Misc Cost")]
        public Nullable<decimal> MiscCost { get; set; }
        public Nullable<decimal> IsOn { get; set; }
        [Display(Name = "Existing Product Name")]
        public string ExistingProductName { get; set; }
        public Nullable<System.Guid> OperatingScheduleKey { get; set; }
        public Nullable<int> SolutionLevel { get; set; }

    }
    public class ReuseSolutionClass
    {
        public Guid? ProductKey { get; set; }
        public string ExsistingName { get; set; }
        public string ProposedName { get; set; }
    }
    public class AreaProductViewClass
    {
        public Guid? AreaKey { get; set; }
        public string AreaName { get; set; }
        public List<ProposalSolutionListClass> ProductList { get; set; }
    }
    public class ProposalSolutionListClass
    {
        public Guid? ProductKey { get; set; }       
        public string ExistingProduct { get; set; }

        public decimal? ExistingCount { get; set; }
        public string ProposedProduct { get; set; }
        public string ProposedCount { get; set; }
        public string OperatingScheduleName { get; set; }
        public Guid? OperatingScheduleKey { get; set; }
        public string OperatingHours { get; set; }
        public string SolutionLevel { get; set; }
        public bool? IsAdd { get; set; }


    }
    public class ProposalViewClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? ProposalKey { get; set; }      
        public string ProposalName { get; set; }
        public string TotalPrice { get; set; }
        public string Incentives { get; set; }
        public string NetPrice { get; set; }
        public string AnnualSaving { get; set; }
        public string ROI { get; set; }


    }
    public class SummaryClass
    {
        public Guid? ProjectKey { get; set; }
        public Guid? CompanyKey { get; set; }
        public string CompanyName { get; set; }
        public Guid? ProposalKey { get; set; }
        public decimal? TotalProjectSaving { get; set; }
        public decimal? SalesTax { get; set; }
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
        public string SimplePaybackYearsString { get; set; }
        public decimal? SimpleROI { get; set; }
        public string SimpleROIstring { get; set; }
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
        [Display(Name = "Monthly Cash Flow")]
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