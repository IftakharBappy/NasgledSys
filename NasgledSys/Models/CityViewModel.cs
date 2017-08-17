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

    public class RoleViewModel
    {
        public Guid? RoleKey { get; set; }
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string RoleName { get; set; }
        [Display(Name = "Level*")]
        
        public Nullable<int> Rlevel { get; set; }
        public Nullable<bool> IsDelete { get; set; }

    }
}