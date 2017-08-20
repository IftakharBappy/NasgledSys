using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtIncentiveMaxType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<IncentiveMaxTypeViewModel> ListAll()
        {
            List<IncentiveMaxTypeViewModel> obj = new List<IncentiveMaxTypeViewModel>();
            var temp = (from x in db.IncentiveMaxType
                        where x.IsDelete == false
                        select new IncentiveMaxTypeViewModel
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(IncentiveMaxTypeViewModel obj)
        {
            int i = 1;
            try
            {
                IncentiveMaxType model = new IncentiveMaxType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.IncentiveMaxType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(IncentiveMaxTypeViewModel obj)
        {
            int i = 1;
            try
            {
                IncentiveMaxType model = db.IncentiveMaxType.Find(obj.PKey);
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
                IncentiveMaxType model = db.IncentiveMaxType.Find(ID);
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