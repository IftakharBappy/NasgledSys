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
        public string Description { get; set; }

        [Display(Name = "AverageIlluminnce")]
        public Nullable<decimal> AverageIlluminnce { get; set; }

        [Display(Name = "LightingSatisfactionKey")]
        public Nullable<System.Guid> LightingSatisfactionKey { get; set; }

        [Display(Name = "CeilingHeight")]
        public Nullable<decimal> CeilingHeight { get; set; }

        [Display(Name = "Reflectance")]
        public Nullable<decimal> Reflectance { get; set; }

        [Display(Name = "AreaWidth")]
        public Nullable<decimal> AreaWidth { get; set; }

        [Display(Name = "Length")]
        public Nullable<decimal> Length { get; set; }

        //public virtual LightingSatisfactionFactor LightingSatisfactionFactor { get; set; }
        //public virtual Area Area { get; set; }
    }
}