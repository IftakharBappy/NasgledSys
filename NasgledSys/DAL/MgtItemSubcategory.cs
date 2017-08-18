using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtItemSubcategory
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<ItemSubcategoryClass> ListAll()
        {
            List<ItemSubcategoryClass> obj = new List<ItemSubcategoryClass>();
            var temp = (from x in db.ItemSubcategory
                        where x.IsDelete == false
                        select new ItemSubcategoryClass
                        {
                            PKey = x.PKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(ItemSubcategoryClass obj)
        {
            int i = 1;
            try
            {
                ItemSubcategory model = new ItemSubcategory();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.ItemSubcategory.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(ItemSubcategoryClass obj)
        {
            int i = 1;
            try
            {
                ItemSubcategory model = db.ItemSubcategory.Find(obj.PKey);
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
                ItemSubcategory model = db.ItemSubcategory.Find(ID);
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