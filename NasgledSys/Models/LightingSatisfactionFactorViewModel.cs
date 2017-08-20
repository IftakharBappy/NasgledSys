using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class LightingSatisfactionFactorViewModel
    {
        public System.Guid PKey { get; set; }
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string FactorName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<LightingSatisfactionFactorViewModel> LightingSatisfactionFactorViewModelList { get; set; }
    }
}