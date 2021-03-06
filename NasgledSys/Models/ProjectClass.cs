﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ExistingClass
    {
        public string Product { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public int? ProductCount { get; set; }
        public Guid? ProductKey { get; set; }
    }
    public class ViewSubAreaList
    {
        public string SubAreaName { get; set; }
        public string AreaName { get; set; }
        public int? ProductCount { get; set; }
        public Guid? AreaKey { get; set; }
    }
    public class ViewAreaList
    {
        public Guid? AreaKey { get; set; }
        public string AreaName { get; set; }
        public int SubArea { get; set; }
    }
    public class AreaClass
    {
        public Guid? AreaKey { get; set; }
        [Display(Name = "Parent Area Name")]
        public string ParentAreaName { get; set; }
        [Display(Name = "Name*...")]
        [Required(ErrorMessage = "Name is required")]
        public string AreaName { get; set; }
        public string Reception { get; set; }
        [Display(Name = "Square Feet")]
        public Nullable<decimal> SquareFeet { get; set; }
        [Display(Name = "Operating Schedule")]
        public Nullable<System.Guid> OperationScheduleKey { get; set; }
        [Display(Name = "Rate Schedule")]
        public Nullable<System.Guid> NewRateScheduleKey { get; set; }
        [Display(Name = "Cooling")]
        public Nullable<System.Guid> CoolingSystemKey { get; set; }
        [Display(Name = "Heating")]
        public Nullable<System.Guid> HeatingSystemKey { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        [Display(Name = "Select Parent Area")]
        public Nullable<System.Guid> ParentAreaKey { get; set; }
        public bool? IsParent { get; set; }
        public bool? IsSubEdit { get; set; }
    }
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
        public int? CityKey { get; set; }
        [Display(Name = "State")]
        public int? StateKey { get; set; }
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
    public class DataReturn
    {
        public int flag { get; set; }
        public Guid? key { get; set; }
        public string mess { get; set; }
    }
}