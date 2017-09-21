using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ScheduleViewModel
    {
        public System.Guid PKey { get; set; }
        [Display(Name = " Name*")]
        [Required(ErrorMessage = " Name is required.")]
        public string OPName { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        [Display(Name = "Annual Operation Hour*")]
        [Required(ErrorMessage = "Annual Operation Hour is required.")]
        public Nullable<decimal> AnnualOperationHour { get; set; }
        [Display(Name = " Internal Note")]
        public string InternalNote { get; set; }
        [Display(Name = " General Note")]
        public string GeneralNote { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    }
}