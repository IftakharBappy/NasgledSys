
using NasgledSys.Models;
using System;
using System.Linq.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using NasgledSys.EM;
using System.Collections.Generic;

namespace NasgledSys.DAL
{
    public class ManageMainProduct
    {
        private NasgledDBEntities db = new NasgledDBEntities();

        public List<ItemViewModel> GetItemList()
        {
            List<ItemViewModel> model = new List<ItemViewModel>();
            try
            {
                var data = (from asset in db.MainProduct
                            select new ItemViewModel
                            {
                                FixtureKey = asset.FixtureKey,
                                ItemTypeKey = asset.ItemTypeKey,
                                CategoryKey = asset.CategoryKey,
                                SubcategoryKey = asset.SubcategoryKey,
                                TypeCount = asset.TypeCount,
                                ManufacturerKey = asset.ManufacturerKey,
                                ProductName = asset.ProductName,
                                ModelNo = asset.ModelNo,
                                Watt = asset.Watt,
                                ThermalEfficacy = asset.ThermalEfficacy,
                                CRI = asset.CRI,
                                Lumen = asset.Lumen,
                                LampLife = asset.LampLife,
                                LightApparent = asset.LightApparent,
                                Source = asset.Source,
                                Brand = asset.Brand,
                                LightOutput = asset.LightOutput,
                                CCT = asset.CCT,
                                Size = asset.Size,
                                Location = asset.Location,
                                MountingBase = asset.MountingBase,
                                Category = asset.CategoryKey == null ? " " : asset.ItemCategory.TypeName,
                               // Catelogue = asset.CatelogueKey == null ? " " : asset.ItemCatelogue.TypeName,
                                Subcategory = asset.SubcategoryKey == null ? " " : asset.ItemSubcategory.TypeName,
                                Type = asset.ItemType.TypeName,
                                Manufacturer = asset.Manufacturer.ManufacturerName
                            }).OrderBy(m => m.ProductName).ToList();
                model = data;
            }
            catch(Exception ex)
            {
                string mess = ex.Message.ToString();
            }
            return model;
        }
        public ItemViewModel GetroductByID(Guid id)
        {
            ItemViewModel model = new ItemViewModel();
            try
            {
                MainProduct asset = db.MainProduct.Find(id);
                model.FixtureKey = asset.FixtureKey;
                model.ItemTypeKey = asset.ItemTypeKey;
                model.CategoryKey = asset.CategoryKey;
                model.SubcategoryKey = asset.SubcategoryKey;
                model.TypeCount = asset.TypeCount;
                model.ManufacturerKey = asset.ManufacturerKey;
                model.ProductName = asset.ProductName;
                model.ModelNo = asset.ModelNo;
                model.Watt = asset.Watt;
                model.ThermalEfficacy = asset.ThermalEfficacy;
                model.CRI = asset.CRI;
                model.Lumen = asset.Lumen;
                model.LampLife = asset.LampLife;
                model.LightApparent = asset.LightApparent;
                model.Source = asset.Source;
                model.Brand = asset.Brand;
                model.LightOutput = asset.LightOutput;
                model.CCT = asset.CCT;
                model.Size = asset.Size;
                model.Location = asset.Location;
                model.MountingBase = asset.MountingBase;
                model.Category = asset.CategoryKey == null ? " " : asset.ItemCategory.TypeName;
                // Catelogue = asset.CatelogueKey == null ? " " : asset.ItemCatelogue.TypeName,
                model.Subcategory = asset.SubcategoryKey == null ? " " : asset.ItemSubcategory.TypeName;
                model.Type = asset.ItemType.TypeName;
                model.Manufacturer = asset.Manufacturer.ManufacturerName;
                model.MainProductDetail = asset.MainItemKey == null ? " " : db.MainProduct.Find(asset.MainItemKey).ProductName;
                           
            
            }
            catch (Exception ex)
            {
                string mess = ex.Message.ToString();
            }
            return model;
        }
    }
}