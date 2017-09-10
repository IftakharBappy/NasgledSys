using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProjectClass
    {
        public Guid? ProjectKey { get; set; }
        [Display(Name = "Project Name*...")]
        [Required(ErrorMessage = "Name is required")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Status*...")]
        [Required(ErrorMessage = "Project Status is required")]
        public Nullable<System.Guid> ProjectStatusKey { get; set; }
        [Display(Name = "Project Status*...")]
        public string StatusName { get; set; }
        [Display(Name = "Primary Client Contact*")]
        [Required(ErrorMessage = "Primary Client Contact is required")]
        public Nullable<System.Guid> PrimaryContactKey { get; set; }
        [Display(Name = "Primary Client Contact")]
        public string ContactName { get; set; }
        [Display(Name = "Prepared By User")]
        public Nullable<System.Guid> PreparedBy { get; set; }
        [Display(Name = "Prepared By User")]
        public string PreparedByDetail { get; set; }
        [Display(Name = "Expected Close Date")]
        public Nullable<int> ExpectedClosingMonth { get; set; }
        public Nullable<int> EspectedClosingYear { get; set; }
        [Display(Name = "Probability Percentage")]
        public Nullable<decimal> ProbabilityOfCompletion { get; set; }
        [Display(Name = "Enable Access For Everyone in this Company")]
        public bool IsAccessableByOthers { get; set; }
        public Nullable<System.Guid> CompanyKey { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        [Display(Name = "Phone")]       
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string OfficePhone { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Address2")]
        public string Address2 { get; set; }
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }
        [Display(Name = "City")]
        public Nullable<int> CityKey { get; set; }
        [Display(Name = "State")]
        public Nullable<int> StateKey { get; set; }
        [Display(Name = "Product Markup Percentage")]
        public decimal? MarkupPercentage { get; set; }
        [Display(Name = "Product Margin")]
        public decimal? ProductMargin { get; set; }
        [Display(Name = "Installation Labor Rate")]
        public decimal? LaborCost { get; set; }
        [Display(Name = "Shipping Cost")]
        public decimal? ShippingCost { get; set; }
        [Display(Name = "Misc Cost")]
        public decimal? MiscCost { get; set; }
        [Display(Name = "179D TAX Incentive")]
        public decimal? TaxIncentives { get; set; }
        [Display(Name = "General Notes")]
        public string GeneralNote { get; set; }
        [Display(Name = "Internal Notes")]
        public string InternalNotes { get; set; }
        public bool? IsDelete { get; set; }
        public Guid? ProfileKey { get; set; }

    }
}