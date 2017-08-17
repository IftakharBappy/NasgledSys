using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtItemType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<ItemTypeClass> ListAll()
        {
            List<ItemTypeClass> obj = new List<ItemTypeClass>();
            var temp = (from x in db.ItemType
                        where x.IsDelete == false
                        select new ItemTypeClass
                        {
                            PKey = x.PKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(ItemTypeClass obj)
        {
            int i = 1;
            try
            {
                ItemType model = new ItemType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.ItemType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(ItemTypeClass obj)
        {
            int i = 1;
            try
            {
                ItemType model = db.ItemType.Find(obj.PKey);
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
                ItemType model = db.ItemType.Find(ID);
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