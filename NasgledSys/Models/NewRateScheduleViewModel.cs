using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class NewRateScheduleViewModel
    {
        public System.Guid PKey { get; set; }
        [Display(Name = " Name*")]
        [Required(ErrorMessage = "Name is Required.")]
        public string NRName { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        [Display(Name = " Blend Electricity Rate*")]
        [Required(ErrorMessage = "Blend Electricity Rate is Required.")]
        public Nullable<decimal> BlendElectricityRate { get; set; }
        [Display(Name = " Internal Note")]
        public string InternalNote { get; set; }
        [Display(Name = " General Note")]
        public string GeneralNote { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    }
}