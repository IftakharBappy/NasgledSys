using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaDetailViewModel
    {

        public System.Guid DetailKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> AverageIlluminnce { get; set; }
        public Nullable<System.Guid> LightingSatisfactionKey { get; set; }
        public Nullable<decimal> CeilingHeight { get; set; }
        public Nullable<decimal> Reflectance { get; set; }
        public Nullable<decimal> AreaWidth { get; set; }
        public Nullable<decimal> Length { get; set; }

        //public virtual LightingSatisfactionFactor LightingSatisfactionFactor { get; set; }
        //public virtual Area Area { get; set; }
    }
}