using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtFuelType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<FuelTypeClass> ListAll()
        {
            List<FuelTypeClass> obj = new List<FuelTypeClass>();
            var temp = (from x in db.FuelType
                        where x.IsDelete == false
                        select new FuelTypeClass
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            Unit=x.Unit,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(FuelTypeClass obj)
        {
            int i = 1;
            try
            {
                FuelType model = new FuelType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.Unit = obj.Unit;
                model.IsDelete = false;
                db.FuelType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(FuelTypeClass obj)
        {
            int i = 1;
            try
            {
                FuelType model = db.FuelType.Find(obj.PKey);
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.Unit = obj.Unit;

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
                FuelType model = db.FuelType.Find(ID);
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