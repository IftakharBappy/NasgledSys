using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtManufacturer
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<ManufacturerClass> ListAll()
        {
            List<ManufacturerClass> obj = new List<ManufacturerClass>();
            var temp = (from x in db.Manufacturer
                        where x.IsDelete == false
                        select new ManufacturerClass
                        {
                            PKey = x.PKey,
                            ManufacturerName = x.ManufacturerName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.ManufacturerName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(ManufacturerClass obj)
        {
            int i = 1;
            try
            {
                Manufacturer model = new Manufacturer();
                model.PKey = Guid.NewGuid();
                model.ManufacturerName = obj.ManufacturerName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.Manufacturer.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(ManufacturerClass obj)
        {
            int i = 1;
            try
            {
                Manufacturer model = db.Manufacturer.Find(obj.PKey);
                model.ManufacturerName = obj.ManufacturerName;
                model.Description = obj.Description;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Delete(Guid ID)
        {
            int i = 1;
            try
            {
                Manufacturer model = db.Manufacturer.Find(ID);
                model.IsDelete = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }
    }
}