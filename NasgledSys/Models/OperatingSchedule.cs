//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NasgledSys.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OperatingSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OperatingSchedule()
        {
            this.AreaProductDetail = new HashSet<AreaProductDetail>();
            this.Area = new HashSet<Area>();
            this.AreaProduct = new HashSet<AreaProduct>();
        }
    
        public System.Guid PKey { get; set; }
        public string OPName { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        public Nullable<decimal> AnnualOperationHour { get; set; }
        public string InternalNote { get; set; }
        public string GeneralNote { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AreaProductDetail> AreaProductDetail { get; set; }
        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Area> Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AreaProduct> AreaProduct { get; set; }
    }
}
