using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaNoteViewModel
    {
        public System.Guid NoteKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public string NoteContent { get; set; }
        public string Internal { get; set; }
    }
}