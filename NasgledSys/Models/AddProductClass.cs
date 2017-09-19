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
}