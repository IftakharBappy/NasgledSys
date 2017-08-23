using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ItemViewModel
    {
        public Guid? FixtureKey { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public Nullable<System.Guid> ItemTypeKey { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public Nullable<System.Guid> CategoryKey { get; set; }
        public Nullable<System.Guid> SubcategoryKey { get; set; }
        [Required(ErrorMessage = "Catelogue is required")]
        public Nullable<System.Guid> CatelogueKey { get; set; }
        [Required(ErrorMessage = "Manufacturer is required")]
        public Nullable<System.Guid> ManufacturerKey { get; set; }
        [Display(Name = "Product Name*")]
        [Required(ErrorMessage = "Name is required")]
        public string ProductName { get; set; }
        [Display(Name = "Model No*")]
        [Required(ErrorMessage = "Model No is required")]
        public string ModelNo { get; set; }
        [Display(Name = "Watt*")]
        [Required(ErrorMessage = "Watt is required")]
        public Nullable<decimal> Watt { get; set; }
        [Display(Name = "Thermal Efficacy*")]
        [Required(ErrorMessage = "Thermal Efficacy is required")]
        public Nullable<decimal> ThermalEfficacy { get; set; }
        [Display(Name = "CRI*")]
        [Required(ErrorMessage = "CRI is required")]
        public Nullable<decimal> CRI { get; set; }
        [Display(Name = "Lumen*")]
        [Required(ErrorMessage = "Lumen is required")]
        public Nullable<decimal> Lumen { get; set; }
        [Display(Name = "Lamp Life*")]
        [Required(ErrorMessage = "Lamp Life is required")]
        public Nullable<decimal> LampLife { get; set; }
        [Display(Name = "Light Appearence*")]
        [Required(ErrorMessage = "Light Appearence is required")]
        public Nullable<decimal> LightApparent { get; set; }
        [Display(Name = "Source")]
        public string Source { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Display(Name = "Light Output*")]
        [Required(ErrorMessage = "Light Output is required")]
        public Nullable<decimal> LightOutput { get; set; }
        [Display(Name = "CCT*")]
        [Required(ErrorMessage = "CCT is required")]
        public Nullable<decimal> CCT { get; set; }
        [Display(Name = "Size")]
        public string Size { get; set; }
        [Display(Name = "Location*")]
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Display(Name = "Mounting Base*")]
        [Required(ErrorMessage = "Mounting Base is required")]
        public string MountingBase { get; set; }
        [Display(Name = "Category*")]
       
        public string Category { get; set; }
        [Display(Name = "Catelogue*")]
     
        public string Catelogue { get; set; }
        [Display(Name = "Subcategory")]
        public string Subcategory { get; set; }
        [Display(Name = "Type*")]
       
        public string Type { get; set; }
        [Display(Name = "Manufacturer*")]
      
        public string Manufacturer { get; set; }
        [Display(Name = "Type Count*")]
        [Required(ErrorMessage = "Please enter the Type Count")]
        public int? TypeCount { get; set; }

        public List<ItemViewModel> ItemList { get; set; }
    }

    public class CatelogueViewModel
    {
        [Display(Name = "Type")]
        public Nullable<System.Guid> ItemTypeKey { get; set; }
        [Display(Name = "Category")]
        public Nullable<System.Guid> CategoryKey { get; set; }
        [Display(Name = "Sub Category")]
        public Nullable<System.Guid> SubcategoryKey { get; set; }
        [Display(Name = "Catelogue")]       
        public Nullable<System.Guid> CatelogueKey { get; set; }
        [Display(Name = "Manufacturer")]
        public Nullable<System.Guid> ManufacturerKey { get; set; }
        
       
        public List<ItemViewModel> ItemList { get; set; }
    }
}