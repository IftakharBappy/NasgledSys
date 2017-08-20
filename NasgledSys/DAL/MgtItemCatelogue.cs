using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtItemCatelogue
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<ItemCatelogueViewModel> ListAll()
        {
            List<ItemCatelogueViewModel> obj = new List<ItemCatelogueViewModel>();
            var temp = (from x in db.ItemCatelogue
                        where x.IsDelete == false
                        select new ItemCatelogueViewModel
                        {
                            PKey = x.PKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(ItemCatelogueViewModel obj)
        {
            int i = 1;
            try
            {
                ItemCatelogue model = new ItemCatelogue();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.ItemCatelogue.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(ItemCatelogueViewModel obj)
        {
            int i = 1;
            try
            {
                ItemCatelogue model = db.ItemCatelogue.Find(obj.PKey);
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
                ItemCatelogue model = db.ItemCatelogue.Find(ID);
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