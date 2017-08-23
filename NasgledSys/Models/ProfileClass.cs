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

        [Display(Name = "Primary Role*..")]
        public string PrimaryRole { get; set; }
        [Required(ErrorMessage = "Please Select Primary Role")]
        [Display(Name = "Primary Role*..")]
        public Nullable<Guid> RoleKey { get; set; }

        [Required(ErrorMessage = "Please Enter your Job Title")]
        [Display(Name = "Job Title*..")]
        public string JobTitle { get; set; }
        [Display(Name = "Phone No*..")]
        [Required(ErrorMessage = "Please Enter your Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Email Add*..")]
        [Required(ErrorMessage = "Please Enter your Email Address")]
        public string Email { get; set; }

        [Display(Name = "Primary Business Type")]
        public string PrimaryBusinessType { get; set; }

        [Display(Name = "Does Your Company Hire Outside Auditors?")]
        public string HireOutsideAuditor { get; set; }
        [Display(Name = "Annual Sales Revenue")]
        public string AnnualSalesRevenue { get; set; }

        [Display(Name = "Sales Reach")]
        public string SalesReach { get; set; }
        [Display(Name = "Manufaterers With Whom You Have A Direct Relation:")]
        public string DirectManufacture { get; set; }
        [Display(Name = "Distributors Through Which You Buy Lighting Products:")]
        public string DirectDistributor { get; set; }
        [Display(Name = "Photo:")]
        public byte[] Photo { get; set; }


    }
}