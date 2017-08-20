using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProfileClass
    {
        public Guid? ProfileKey { get; set; }
        [Required(ErrorMessage = "Please Enter your First Name")]
        [Display(Name = "First Name*..")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter your Last Name")]
        [Display(Name = "Last Name*..")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter your Company Name")]
        [Display(Name = "Company Name*..")]
        public string CompanyName { get; set; }
       
        [Display(Name = "Location*..")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please Select Location")]
        [Display(Name = "Location*..")]
        public Nullable<int> CityKey { get; set; }
        [Required(ErrorMessage = "Please Enter your Job Title")]
        [Display(Name = "Job Title*..")]
        public string JobTitle { get; set; }
       
    }
}