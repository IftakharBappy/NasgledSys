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
    
    public partial class ProjectStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectStatus()
        {
            this.Project = new HashSet<Project>();
        }
    
        public System.Guid ProjectStatusKey { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project { get; set; }
    }
}