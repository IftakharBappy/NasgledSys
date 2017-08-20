using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtIndustryType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<IndustryTypeViewModel> ListAll()
        {
            List<IndustryTypeViewModel> obj = new List<IndustryTypeViewModel>();
            var temp = (from x in db.IndustryType
                        where x.IsDelete == false
                        select new IndustryTypeViewModel
                        {
                            IndustryKey = x.IndustryKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(IndustryTypeViewModel obj)
        {
            int i = 1;
            try
            {
                IndustryType model = new IndustryType();
                model.IndustryKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.IndustryType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(IndustryTypeViewModel obj)
        {
            int i = 1;
            try
            {
                IndustryType model = db.IndustryType.Find(obj.IndustryKey);
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
                IndustryType model = db.IndustryType.Find(ID);
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