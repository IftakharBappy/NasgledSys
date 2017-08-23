using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtSalesReachSetup
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<SalesReachSetupViewModel> ListAll()
        {
            List<SalesReachSetupViewModel> obj = new List<SalesReachSetupViewModel>();
            var temp = (from x in db.SalesReachSetup
                        where x.IsDelete == false
                        select new SalesReachSetupViewModel
                        {
                            PKey = x.PKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(SalesReachSetupViewModel obj)
        {
            int i = 1;
            try
            {
                SalesReachSetup model = new SalesReachSetup();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.SalesReachSetup.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(SalesReachSetupViewModel obj)
        {
            int i = 1;
            try
            {
                SalesReachSetup model = db.SalesReachSetup.Find(obj.PKey);
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
                SalesReachSetup model = db.SalesReachSetup.Find(ID);
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