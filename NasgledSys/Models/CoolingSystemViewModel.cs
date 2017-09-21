using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class CoolingSystemViewModel
    {
        public System.Guid CoolingSystemKey { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        [Display(Name = " Name*")]
        [Required(ErrorMessage = "Name is Required.")]
        public string SystemName { get; set; }
        [Display(Name = " Annual Run Time*")]
        [Required(ErrorMessage = "Annual Run Time is Required.")]
        public Nullable<decimal> AnnualRunTime { get; set; }
        [Display(Name = " Cooling System Type*")]
        [Required(ErrorMessage = "Cooling System Type is Required.")]
        public Nullable<System.Guid> CoolingSystemTypeKey { get; set; }
        [Display(Name = " Fuel Type*")]
        [Required(ErrorMessage = "Fuel Type is Required.")]
        public Nullable<System.Guid> FuelTypeKey { get; set; }
        [Display(Name = " Fuel Cost*")]
        [Required(ErrorMessage = "Fuel Cost is Required.")]
        public Nullable<decimal> FuelCost { get; set; }
        [Display(Name = " Efficiency Type*")]
        [Required(ErrorMessage = "Efficiency Type is Required.")]
        public Nullable<System.Guid> EfficiencyType { get; set; }
        [Display(Name = " Efficiency Value*")]
        [Required(ErrorMessage = "Efficiency Value is Required.")]
        public Nullable<decimal> EfficiencyValue { get; set; }
        [Display(Name = " Internal Note")]
        public string InternalNote { get; set; }
        [Display(Name = " General Note")]
        public string GeneralNote { get; set; }
        public Nullable<bool> IsDefault { get; set; }

        public virtual CoolingEfficientyType CoolingEfficientyType { get; set; }
        public virtual CoolingSystemType CoolingSystemType { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual Project Project { get; set; }
    }
}