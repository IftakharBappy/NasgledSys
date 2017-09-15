using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class ProfileProductViewModel
    {
        //[Display(Name = "Type*")]
        //[Required(ErrorMessage = "Name is required")]
        public System.Guid FixtureKey { get; set; }

        [Display(Name = "Item Type*")]
        [Required(ErrorMessage = "Item is required")]
        public Nullable<System.Guid> ItemTypeKey { get; set; }

        [Display(Name = "Category*")]
        [Required(ErrorMessage = "Category is required")]
        public Nullable<System.Guid> CategoryKey { get; set; }

        [Display(Name = "Subcategory*")]
        [Required(ErrorMessage = "Subcategory is required")]
        public Nullable<System.Guid> SubcategoryKey { get; set; }

        [Display(Name = "Catelogue*")]
        [Required(ErrorMessage = "Catelogue is required")]
        public Nullable<System.Guid> CatelogueKey { get; set; }

        [Display(Name = "Manufacturer*")]
        [Required(ErrorMessage = "Manufacturer is required")]
        public Nullable<System.Guid> ManufacturerKey { get; set; }

        [Display(Name = "Product Name*")]
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Display(Name = "Model No*")]
        [Required(ErrorMessage = "Model number is required")]
        public string ModelNo { get; set; }

        [Display(Name = "Watts Per Product*")]
        [Required(ErrorMessage = "Watt is required")]
        public Nullable<decimal> Watt { get; set; }

        [Display(Name = "Thermal Efficacy")]
        public Nullable<decimal> ThermalEfficacy { get; set; }

        [Display(Name = "CRI")]
        public Nullable<decimal> CRI { get; set; }

        [Display(Name = "Lumen")]
        public Nullable<decimal> Lumen { get; set; }

        [Display(Name = "Light Apparent")]
        public Nullable<decimal> LightApparent { get; set; }

        [Display(Name = "Source*")]
        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; }

        [Display(Name = "Brand*")]
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        [Display(Name = "Light Output")]
        public Nullable<decimal> LightOutput { get; set; }

        [Display(Name = "CCT")]
        public Nullable<decimal> CCT { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Mounting Base*")]
        [Required(ErrorMessage = "Mounting base is required")]
        public string MountingBase { get; set; }

        public Nullable<System.Guid> ProfileKey { get; set; }

        //public Nullable<HttpPostedFileBase> Logo { get; set; }
        public byte[] Logo { get; set; }
        public bool KeepOldfile { get; set; }

        public string FileType { get; set; }
        public string FileName { get; set; }

        [Display(Name = "Lamp Life")]
        public Nullable<decimal> LampLife { get; set; }

        [Display(Name = "How many type variation does this Product have")]
        public Nullable<int> TypeCount { get; set; }

        public Guid? MainItemKey { get; set; }
        [Display(Name = "Does it Fall under another Product")]
        public string MainProductDetail { get; set; }
    }
}