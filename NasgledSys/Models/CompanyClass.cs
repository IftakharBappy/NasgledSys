using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class CompanyClass
    {
        public System.Guid CompanyKey { get; set; }
        [Display(Name = "ID")]
        public string CompanyID { get; set; }
        [Display(Name = "Company Name*")]
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }
        [Display(Name = "Address")]
        public string CompanyAddress { get; set; }
        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "Telephone Number is required")]
        public string CompanyPhone { get; set; }
        [Display(Name = "Alternate Contact #")]
        public string CompanyMobile { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string CompanyEmail { get; set; }
        [Display(Name = "Website")]
        public string CompanyWebsite { get; set; }
        [Display(Name = "Fax")]
        public string CompanyFax { get; set; }
        [Display(Name = "Represented By")]
        public string ContactPersonName { get; set; }
        [Display(Name = "Telephone")]
        public string ContactPersonNo { get; set; }
        public byte[] Logo { get; set; }
        public string LogoType { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        [Required(ErrorMessage = "The State name is required")]
        public Nullable<int> StateCode { get; set; }
        [Required(ErrorMessage = "The City Name is required")]
        public Nullable<int> CityKey { get; set; }
        [Required(ErrorMessage = "The Zipcode is required")]
        public int? Zipcode { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string ContactEmail { get; set; }
    }

    public class ModuleClass
    {
        public System.Guid ModuleID { get; set; }
      
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string ModuleName { get; set; }
        [Display(Name = "Level")]
        [Required(ErrorMessage = "Level is required")]
        public int? ModuleLevel { get; set; }
        [Display(Name = "Has Child")]
        [Required(ErrorMessage = "Please Select an Option")]
        public bool? NoSub { get; set; }
        public Guid? ParentModuleID { get; set; }

        [Display(Name = "Main Module Name")]
        public string MainModuleName { get; set; }
        public string sub { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
     
    }
    [MetadataType(typeof(StateMetaData))]
    public partial class StateList
    {
        



    }
    public class StateMetaData
    {
        public int PKey { get; set; }
        [Required(AllowEmptyStrings=false, ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string StateName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Short Name is required")]
        [Display(Name = "Shortname/ Code")]
        public string StateCode { get; set; }
        public Nullable<bool> IsDelete { get; set; }
       


    }
}