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
        [Required(ErrorMessage = "Factor Name name is required.")]
        public string FactorName { get; set; }
        public string UnitName { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Only Decimal Value , like 12.20")]
        [Range(0, 9999999999999999.99)]
        public Nullable<decimal> QtyUsed { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Only Decimal Value , like 12.20")]
        [Range(0, 9999999999999999.99)]
        public Nullable<decimal> KilowattSaved { get; set; }
        public byte[] Logo { get; set; }
        public string LogoType { get; set; }
        public string Filename { get; set; }
        [Display(Name = "Logo")]
        public HttpPostedFileBase PostedLogo { get; set; }

        public List<EnvironmentalImpactViewModel> EnvironmentalImpactViewModelList { get; set; }
    }
}