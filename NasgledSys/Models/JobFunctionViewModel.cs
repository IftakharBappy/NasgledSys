using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class JobFunctionViewModel
    {
        public System.Guid JobFunctionKey { get; set; }
        [Display(Name = "Function*")]
        [Required(ErrorMessage = "Name is required")]
        public string FunctionName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<JobFunctionViewModel> JobFunctionViewModelList { get; set; }
    }
}