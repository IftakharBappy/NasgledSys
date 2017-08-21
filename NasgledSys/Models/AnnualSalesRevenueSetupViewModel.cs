using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace NasgledSys.Models
{
    public class AnnualSalesRevenueSetupViewModel
    {
        public System.Guid PKey { get; set; }
        [Display(Name = "Type*")]
        [Required(ErrorMessage = "Name is required")]
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<AnnualSalesRevenueSetupViewModel> AnnualSalesRevenueSetupViewModelList { get; set; }
    }
}