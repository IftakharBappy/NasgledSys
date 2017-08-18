using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasgledSys.Models;
namespace NasgledSys.EM
{
    public static class EM_MainProduct
    {
        public static ItemViewModel ConvertToModel(MainProduct ent)
        {
            ItemViewModel bo = new ItemViewModel();

            bo.FixtureKey = (Guid)ent.FixtureKey;
            bo.ItemTypeKey = ent.ItemTypeKey;
            bo.CategoryKey = ent.CategoryKey;
            bo.SubcategoryKey = ent.SubcategoryKey;
            bo.CatelogueKey = ent.CatelogueKey;
            bo.ManufacturerKey = ent.ManufacturerKey;
            bo.ProductName = ent.ProductName;
            bo.ModelNo = ent.ModelNo;
            bo.Watt = ent.Watt;
            bo.ThermalEfficacy = ent.ThermalEfficacy;
            bo.CRI = ent.CRI;
            bo.Lumen = ent.Lumen;
            bo.LampLife = ent.LampLife;
            bo.LightApparent = ent.LightApparent;
            bo.Source = ent.Source;
            bo.Brand = ent.Brand;
            bo.LightOutput = ent.LightOutput;
            bo.CCT = ent.CCT;
            bo.Size = ent.Size;
            bo.Location = ent.Location;
            bo.MountingBase = ent.MountingBase;
          
            return bo;
        }

        public static MainProduct ConvertToEntity(ItemViewModel ent)
        {
            MainProduct bo = new MainProduct();

            bo.FixtureKey = (Guid)ent.FixtureKey;
            bo.ItemTypeKey = ent.ItemTypeKey;
            bo.CategoryKey = ent.CategoryKey;
            bo.SubcategoryKey = ent.SubcategoryKey;
            bo.CatelogueKey = ent.CatelogueKey;
            bo.ManufacturerKey = ent.ManufacturerKey;
            bo.ProductName = ent.ProductName;
            bo.ModelNo = ent.ModelNo;
            bo.Watt = ent.Watt;
            bo.ThermalEfficacy = ent.ThermalEfficacy;
            bo.CRI = ent.CRI;
            bo.Lumen = ent.Lumen;
            bo.LampLife = ent.LampLife;
            bo.LightApparent = ent.LightApparent;
            bo.Source = ent.Source;
            bo.Brand = ent.Brand;
            bo.LightOutput = ent.LightOutput;
            bo.CCT = ent.CCT;
            bo.Size = ent.Size;
            bo.Location = ent.Location;
            bo.MountingBase = ent.MountingBase;

            return bo;
        }

        public static MainProduct FillEntityForEdit(MainProduct fromdb,MainProduct fromForm)
        {
                 
            fromdb.ItemTypeKey = fromForm.ItemTypeKey;
            fromdb.CategoryKey = fromForm.CategoryKey;
            fromdb.SubcategoryKey = fromForm.SubcategoryKey;
            fromdb.CatelogueKey = fromForm.CatelogueKey;
            fromdb.ManufacturerKey = fromForm.ManufacturerKey;
            fromdb.ProductName = fromForm.ProductName;
            fromdb.ModelNo = fromForm.ModelNo;
            fromdb.Watt = fromForm.Watt;
            fromdb.ThermalEfficacy = fromForm.ThermalEfficacy;
            fromdb.CRI = fromForm.CRI;
            fromdb.Lumen = fromForm.Lumen;
            fromdb.LampLife = fromForm.LampLife;
            fromdb.LightApparent = fromForm.LightApparent;
            fromdb.Source = fromForm.Source;
            fromdb.Brand = fromForm.Brand;
            fromdb.LightOutput = fromForm.LightOutput;
            fromdb.CCT = fromForm.CCT;
            fromdb.Size = fromForm.Size;
            fromdb.Location = fromForm.Location;
            fromdb.MountingBase = fromForm.MountingBase;

            return fromdb;
        }

    }
}