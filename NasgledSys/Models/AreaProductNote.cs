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
    
    public partial class AreaProductNote
    {
        public System.Guid NoteKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public Nullable<System.Guid> FixtureKey { get; set; }
        public Nullable<System.Guid> ProductKey { get; set; }
        public string Condition { get; set; }
        public string InternalNotes { get; set; }
        public string GeneralNote { get; set; }
        public string Description { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual AreaProduct AreaProduct { get; set; }
        public virtual ProfileProduct ProfileProduct { get; set; }
    }
}
