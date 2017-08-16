using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class CityViewModel
    {
        public int? CityKey { get; set; }
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string CityName { get; set; }
        [Display(Name = "State*")]

        public string StateName { get; set; }

        public Nullable<int> StateCode { get; set; }
        public Nullable<bool> IsDelete { get; set; }
      
    }
}