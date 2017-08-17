using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtItemCategory
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<ItemCategoryClass> ListAll()
        {
            List<ItemCategoryClass> obj = new List<ItemCategoryClass>();
            var temp = (from x in db.ItemCategory
                        where x.IsDelete == false
                        select new ItemCategoryClass
                        {
                            PKey = x.PKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(ItemCategoryClass obj)
        {
            int i = 1;
            try
            {
                ItemCategory model = new ItemCategory();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.ItemCategory.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(ItemCategoryClass obj)
        {
            int i = 1;
            try
            {
                ItemCategory model = db.ItemCategory.Find(obj.PKey);
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
                ItemCategory model = db.ItemCategory.Find(ID);
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