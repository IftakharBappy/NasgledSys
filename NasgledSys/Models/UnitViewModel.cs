using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class UnitViewModel
    {
        public System.Guid UnitKey { get; set; }
        public string UnitName { get; set; }

        [Display(Name = "Unit Short name")]
        [Required(ErrorMessage = "Unit Short name is required.")]
        public string UnitShortname { get; set; }

        public List<UnitViewModel> UnitViewModelList { get; set; }
    }
}