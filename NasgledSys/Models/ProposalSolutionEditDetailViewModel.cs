using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NasgledSys.Models
{
    public class ProposalSolutionEditDetailViewModel
    {
        public Guid ProductKey { get; set; }
        public Guid ProposalKey { get; set; }

        [Display(Name = "Apply To")]
        public string ProfileProductNameText { get; set; }

        [Display(Name = "Name")]
        public string SolutionName { get; set; }


        [Display(Name = "Incentive Type")]
        public Nullable<System.Guid> IncentiveTypeKey { get; set; }

        [Display(Name = "Incentive Amount")]
        public Nullable<decimal> IncentiveAmount { get; set; }

        [Display(Name = "Incentive Maximum Type")]
        public Nullable<System.Guid> IncentiveMaxTypeKey { get; set; }

        [Display(Name = "Incentive Maximum Amount")]
        public Nullable<decimal> IncentiveMaxAmount { get; set; }

        [Display(Name = "Product Cost")]
        public Nullable<decimal> ProductCost { get; set; }

        [Display(Name = "Markup Percent")]
        public Nullable<decimal> MarkupPercentage { get; set; }

        [Display(Name = "Installation Time")]
        public Nullable<decimal> InstallationTime { get; set; }

        [Display(Name = "Installation Cost")]
        public Nullable<decimal> InstallationCost { get; set; }

        [Display(Name = "Misc Cost")]
        public Nullable<decimal> MiscCost { get; set; }

        //ProductName
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Model No")]
        public string ModelNo { get; set; }

        [Display(Name = "Watt Per Product")]
        public decimal? WattPerProduct { get; set; }

        [Display(Name = "Thermal Efficiency")]
        public decimal? ThermalEfficency { get; set; }


    }
}