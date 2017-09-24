using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaDetailViewModel
    {

        public System.Guid DetailKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Average Illuminance")]
        public Nullable<decimal> AverageIlluminnce { get; set; }

        [Display(Name = "Lighting Satisfaction")]
        [Required(ErrorMessage = "Lighting Satisfaction is required")]
        public Nullable<System.Guid> LightingSatisfactionKey { get; set; }

        [Display(Name = "Ceiling Height")]
        public Nullable<decimal> CeilingHeight { get; set; }

        [Display(Name = "Reflectance")]
        public Nullable<decimal> Reflectance { get; set; }

        [Display(Name = "Area Width")]
        public Nullable<decimal> AreaWidth { get; set; }

        [Display(Name = "Length")]
        public Nullable<decimal> Length { get; set; }

      
    }
}