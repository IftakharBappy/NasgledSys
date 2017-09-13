using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class EnvironmentalImpactViewModel
    {
        public System.Guid FactorKey { get; set; }
        [Display(Name = "Factor Name")]
        [Required(ErrorMessage = "Facto rName name is required.")]
        public string FactorName { get; set; }
        public string UnitName { get; set; }
        public Nullable<decimal> QtyUsed { get; set; }
        public Nullable<decimal> KilowattSaved { get; set; }
        public byte[] Logo { get; set; }
        public string LogoType { get; set; }
        public string Filename { get; set; }

        public List<EnvironmentalImpactViewModel> EnvironmentalImpactViewModelList { get; set; }
    }
}