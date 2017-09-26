using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaProductDocumentViewModel
    {
        public System.Guid DocumentKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public Nullable<System.Guid> FixtureKey { get; set; }
        public Nullable<System.Guid> ProductKey { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string Description { get; set; }

        public List<AreaProductDocumentViewModel> AreaProductDocumentList { get; set; }
    }
}