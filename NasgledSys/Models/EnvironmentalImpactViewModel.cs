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
        [Required(ErrorMessage = "Factor Name is required.")]
        public string FactorName { get; set; }
        [Display(Name = "Unit Name")]
        [Required(ErrorMessage = "Unit name is required.")]
        public string UnitName { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Display(Name = "Quantity Used")]
        public Nullable<decimal> QtyUsed { get; set; }
        [Required(ErrorMessage = "KiloWatt Saved is required.")]
        [Display(Name = "KiloWatt Saved")]
        public Nullable<decimal> KilowattSaved { get; set; }
        public byte[] Logo { get; set; }
        public string LogoType { get; set; }
        public string Filename { get; set; }
        [Display(Name = "Logo")]
        public HttpPostedFileBase PostedLogo { get; set; }

        public List<EnvironmentalImpactViewModel> EnvironmentalImpactViewModelList { get; set; }
    }
}