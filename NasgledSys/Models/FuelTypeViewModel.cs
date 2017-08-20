using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class FuelTypeViewModel
    {
        public System.Guid PKey { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<FuelTypeViewModel> FuelTypeViewModelList { get; set; }
    }
}