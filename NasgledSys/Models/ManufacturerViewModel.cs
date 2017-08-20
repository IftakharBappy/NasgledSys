using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ManufacturerViewModel
    {
        public System.Guid PKey { get; set; }
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public virtual List<ManufacturerViewModel> ManufacturerViewModelList { get; set; }
    }
}