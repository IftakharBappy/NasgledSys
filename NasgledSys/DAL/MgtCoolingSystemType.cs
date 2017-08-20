using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtCoolingSystemType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<CoolingSystemTypeViewModel> ListAll()
        {
            List<CoolingSystemTypeViewModel> obj = new List<CoolingSystemTypeViewModel>();
            var temp = (from x in db.CoolingSystemType
                        where x.IsDelete == false
                        select new CoolingSystemTypeViewModel
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(CoolingSystemTypeViewModel obj)
        {
            int i = 1;
            try
            {
                CoolingSystemType model = new CoolingSystemType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.CoolingSystemType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(CoolingSystemTypeViewModel obj)
        {
            int i = 1;
            try
            {
                CoolingSystemType model = db.CoolingSystemType.Find(obj.PKey);
                model.TypeName = obj.TypeName;
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
                CoolingSystemType model = db.CoolingSystemType.Find(ID);
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