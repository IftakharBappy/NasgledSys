using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class HeatingSystemViewModel
    {
        public System.Guid HeatingSystemKey { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        [Display(Name = " Name*")]
        [Required(ErrorMessage = "Name is Required.")]
        public string SystemName { get; set; }
        [Display(Name = " Annual RunTime")]
        [Required(ErrorMessage = "Annual RunTime is Required.")]
        public Nullable<decimal> AnnualRunTime { get; set; }
        [Display(Name = " Heating System Type")]
        [Required(ErrorMessage = "Heating System is Required.")]
        public Nullable<System.Guid> HeatingSystemTypeKey { get; set; }
        [Display(Name = " Fuel Type")]
        [Required(ErrorMessage = "Fuel Type is Required.")]
        public Nullable<System.Guid> FuelTypeKey { get; set; }
        [Display(Name = " Fuel Cost")]
        [Required(ErrorMessage = "Name is Required.")]
        public Nullable<decimal> FuelCost { get; set; }
        [Display(Name = " Efficiency Type")]
        [Required(ErrorMessage = "Efficiency Type is Required.")]
        public Nullable<System.Guid> EfficiencyType { get; set; }
        [Display(Name = " Efficiency Value")]
        [Required(ErrorMessage = "Efficiency Value is Required.")]
        public Nullable<decimal> EfficiencyValue { get; set; }
        [Display(Name = " Internal Note")]
        public string InternalNote { get; set; }
        [Display(Name = " General Note")]
        public string GeneralNote { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        [Display(Name = " Fuel Type")]
        public virtual FuelType FuelType { get; set; }
        public virtual HeatingEfficiencyType HeatingEfficiencyType { get; set; }
        public virtual HeatingSystemType HeatingSystemType { get; set; }
        public virtual Project Project { get; set; }
    }
}