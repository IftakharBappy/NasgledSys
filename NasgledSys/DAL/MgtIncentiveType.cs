using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtIncentiveType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<IncentiveTypeViewModel> ListAll()
        {
            List<IncentiveTypeViewModel> obj = new List<IncentiveTypeViewModel>();
            var temp = (from x in db.IncentiveType
                        where x.IsDelete == false
                        select new IncentiveTypeViewModel
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(IncentiveTypeViewModel obj)
        {
            int i = 1;
            try
            {
                IncentiveType model = new IncentiveType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.IncentiveType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(IncentiveTypeViewModel obj)
        {
            int i = 1;
            try
            {
                IncentiveType model = db.IncentiveType.Find(obj.PKey);
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
                IncentiveType model = db.IncentiveType.Find(ID);
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