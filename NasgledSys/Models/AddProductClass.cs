using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NasgledSys.Models
{
    public class BrowseItemClass
    {
        public Guid? FixtureKey { get; set; }
       
        public int? TypeCount { get; set; }
        
        public string ProductName { get; set; }
       
        public string ModelNo { get; set; }
       
        public Guid? MainItemKey { get; set; }       
    }
    public class AddProductClass
    {
       
        public Guid? ProductKey { get; set; }
        [Display(Name = "Product Area")]
        [Required(ErrorMessage = "Area is required")]
        public Guid? AreaKey { get; set; }
        [Display(Name = "Product Type")]
        [Required(ErrorMessage = "Product Type is required")]
        public Guid? FixtureKey { get; set; }
        [Display(Name = "Count")]
        [Required(ErrorMessage = "Count is required")]
        public int? Count { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public Area AreaObj { get; set; }
    }

    public class ProductDetailClass
    {

        public Guid? ProductKey { get; set; }
        [Display(Name = "Product Area")]
        [Required(ErrorMessage = "Area is required")]
        public Guid? AreaKey { get; set; }
        [Display(Name = "Product Type")]
        [Required(ErrorMessage = "Product Type is required")]
        public Guid? FixtureKey { get; set; }


        public System.Guid DetailKey { get; set; }
        [Display(Name = "Existing Control")]
        public Nullable<bool> ExistingControl { get; set; }
        [Display(Name = "Operating Schedule")]
        [Required(ErrorMessage = "Operating Schedule is required")]
        public Nullable<System.Guid> OperationScheduleKey { get; set; }
        [Display(Name = "Working Fixture Count")]
        public Nullable<int> WorkingFixtureCount { get; set; }
        [Display(Name = "Replacement Cost")]
        public Nullable<decimal> ReplacementCost { get; set; }
        [Display(Name = "Annual Maintenance")]
        public Nullable<decimal> AnnualMaintenance { get; set; }
        [Display(Name = "Installation Time")]
        public Nullable<decimal> InstallationInTime { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Year")]
        public Nullable<int> Year { get; set; }
        [Display(Name = "Lighting Satisfaction")]
        public Nullable<System.Guid> LightingSatisfactionKey { get; set; }
        [Display(Name = "Mounting Height")]
        public Nullable<decimal> MountingHeight { get; set; }

      
    }

    public class ProductNoteClass
    {

        public Guid? ProductKey { get; set; }
        [Display(Name = "Product Area")]
        [Required(ErrorMessage = "Area is required")]
        public Guid? AreaKey { get; set; }
        [Display(Name = "Product Type")]
        [Required(ErrorMessage = "Product Type is required")]
        public Guid? FixtureKey { get; set; }


        public System.Guid NoteKey { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Condition")]
        public string Condition { get; set; }
        [Display(Name = "Internal Notes")]
        public string InternalNotes { get; set; }
        [Display(Name = "Notes")]
        public string GeneralNote { get; set; }



    }
}