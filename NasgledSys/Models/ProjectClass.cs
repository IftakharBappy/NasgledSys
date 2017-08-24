using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProjectClass
    {
        public Guid? ProjectKey { get; set; }
        [Display(Name = "Project Name*...")]
        [Required(ErrorMessage = "Name is required")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Status*...")]
        [Required(ErrorMessage = "Project Status is required")]
        public Nullable<System.Guid> ProjectStatusKey { get; set; }
        [Display(Name = "Project Status*...")]
        public string StatusName { get; set; }
        [Display(Name = "Primary Client Contact")]
        public Nullable<System.Guid> PrimaryContactKey { get; set; }
        [Display(Name = "Primary Client Contact")]
        public string ContactName { get; set; }
        [Display(Name = "Prepared By User")]
        public Nullable<System.Guid> PreparedBy { get; set; }
        [Display(Name = "Prepared By User")]
        public string PreparedByDetail { get; set; }
        [Display(Name = "Expected Close Date")]
        public Nullable<int> ExpectedClosingMonth { get; set; }
        public Nullable<int> EspectedClosingYear { get; set; }
        [Display(Name = "Probability Percentage")]
        public Nullable<decimal> ProbabilityOfCompletion { get; set; }
        [Display(Name = "Enable Access For Everyone in this Company")]
        public bool IsAccessableByOthers { get; set; }
        public Nullable<System.Guid> CompanyKey { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }

       
    }
}