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
    
    public partial class ProposalNote
    {
        public System.Guid NoteKey { get; set; }
        public Nullable<System.Guid> ProjectKey { get; set; }
        public Nullable<System.Guid> ProposalKey { get; set; }
        public string InternalNotes { get; set; }
    }
}
