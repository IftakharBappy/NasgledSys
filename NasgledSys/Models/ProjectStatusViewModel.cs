using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProjectStatusViewModel
    {
        public System.Guid ProjectStatusKey { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<ProjectStatusViewModel> ProjectStatusViewModelList { get; set; }
    }
}