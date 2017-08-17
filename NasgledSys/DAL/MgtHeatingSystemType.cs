using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtHeatingSystemType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<HeatingSystemTypeClass> ListAll()
        {
            List<HeatingSystemTypeClass> obj = new List<HeatingSystemTypeClass>();
            var temp = (from x in db.HeatingSystemType
                        where x.IsDelete == false
                        select new HeatingSystemTypeClass
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(HeatingSystemTypeClass obj)
        {
            int i = 1;
            try
            {
                HeatingSystemType model = new HeatingSystemType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.HeatingSystemType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(HeatingSystemTypeClass obj)
        {
            int i = 1;
            try
            {
                HeatingSystemType model = db.HeatingSystemType.Find(obj.PKey);
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
                HeatingSystemType model = db.HeatingSystemType.Find(ID);
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