using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class AreaDocumentViewModel
    {
        public Nullable<System.Guid> AreaKey { get; set; }
        public System.Guid DocumentKey { get; set; }
        public byte[] FileContent { get; set; }

        public string Description { get; set; }

        public List<AreaDocumentModel> AreaDocumentList { get; set; }
    }
}