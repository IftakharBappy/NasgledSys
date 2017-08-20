using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ItemSubcategoryViewModel
    {
        public System.Guid PKey { get; set; }
        [Display(Name = "Subcategory*")]
        [Required(ErrorMessage = "Name is required")]
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<ItemSubcategoryViewModel> ItemSubcategoryViewModelList { get; set; }
    }
}