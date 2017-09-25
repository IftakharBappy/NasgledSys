using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaDocumentModel
    {
        public System.Guid DocumentKey { get; set; }
        public Nullable<System.Guid> AreaKey { get; set; }
        public string FileType { get; set; }
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }
}