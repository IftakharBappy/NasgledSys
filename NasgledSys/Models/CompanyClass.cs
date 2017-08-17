using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class StaffClass
    {
        public System.Guid PersonnelKey { get; set; }
        [Display(Name = "ID")]
        public string PID { get; set; }
        [Display(Name = "Full Name*")]
        [Required(ErrorMessage = "Full Name is required")]
        public string PName { get; set; }
        [Display(Name = "Contact No*")]
        [Required(ErrorMessage = "Contact No is required")]
        public string Mobile { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Mail { get; set; }
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Display(Name = "Department")]
        public string Department { get; set; }
        public Nullable<System.Guid> DepartmentKey { get; set; }
        public byte[] Pic { get; set; }
        public string PicType { get; set; }
        public Nullable<System.Guid> CompanyKey { get; set; }
        [Display(Name = "Usergroup*")]
        [Required(ErrorMessage = "Usergroup is not selected Correctly")]
        public Nullable<System.Guid> Usergr { get; set; }
        [Display(Name = "Username*")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password*")]
        [Required(ErrorMessage = "Please enter the Password again.")]
        [Compare("Password", ErrorMessage = "Your Confirm Password does not match.")]
        public string ConfirmPassword { get; set; }
        public Nullable<bool> IsUser { get; set; }
        public virtual Usergroup Usergroup { get; set; }
    }
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
   
    public class CityClass
    {
        public int CityKey { get; set; }
        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string CityName { get; set; }
        [Display(Name = "State*")]
       
        public string StateName { get; set; }
       
        public Nullable<int> StateCode { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<CityClass> CityList { get; set; }
    }

    public class CoolingEfficiencyTypeClass
    {
        public System.Guid PKey { get; set; }
        [Display(Name = "Type*")]
        [Required(ErrorMessage = "Name is required")]
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<CoolingEfficiencyTypeClass> CoolingEfficiencyTypeList { get; set; }
    }

    public class CoolingSystemTypeClass
    {
        public System.Guid PKey { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<CoolingSystemTypeClass> CoolingSystemTypeList { get; set; }
    }

    public class IndustryTypeClass
    {
        public System.Guid IndustryKey { get; set; }
        [Display(Name = "Type*")]
        [Required(ErrorMessage = "Name is required")]
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<IndustryTypeClass> IndustryTypeClassList { get; set; }
    }
    public  class ItemCategoryClass
    {
        public Guid? PKey { get; set; }
        [Display(Name = "Type*")]
        [Required(ErrorMessage = "Name is required")]
        public string TypeName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public List<ItemCategoryClass> CategoryList { get; set; }
        
    }
    public class FuelTypeClass
    {
        public System.Guid PKey { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<FuelTypeClass> FuelTypeClassList { get; set; }
    }

    public class HeatingEfficiencyTypeClass
    {
        public System.Guid PKey { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<HeatingEfficiencyTypeClass> HeatingEfficiencyTypeList { get; set; }
    }

}