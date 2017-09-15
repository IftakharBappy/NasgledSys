using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProjectThumbnailClass
    {
     public Guid? ProjectKey { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectStatus { get; set; }
        public string AdminName { get; set; }
        public int AreaNum { get; set; }
        public int? ExsistingProduct { get; set; }
    }
}