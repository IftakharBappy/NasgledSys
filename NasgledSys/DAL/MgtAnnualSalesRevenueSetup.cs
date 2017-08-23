using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtAnnualSalesRevenueSetup
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<AnnualSalesRevenueSetupViewModel> ListAll()
        {
            List<AnnualSalesRevenueSetupViewModel> obj = new List<AnnualSalesRevenueSetupViewModel>();
            var temp = (from x in db.AnnualSalesRevenueSetup
                        where x.IsDelete == false
                        select new AnnualSalesRevenueSetupViewModel
                        {
                            PKey = x.PKey,
                            TypeName = x.TypeName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(AnnualSalesRevenueSetupViewModel obj)
        {
            int i = 1;
            try
            {
                AnnualSalesRevenueSetup model = new AnnualSalesRevenueSetup();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.AnnualSalesRevenueSetup.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(AnnualSalesRevenueSetupViewModel obj)
        {
            int i = 1;
            try
            {
                AnnualSalesRevenueSetup model = db.AnnualSalesRevenueSetup.Find(obj.PKey);
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
                AnnualSalesRevenueSetup model = db.AnnualSalesRevenueSetup.Find(ID);
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