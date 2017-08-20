using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtLightingSatisfactionFactor
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<LightingSatisfactionFactorViewModel> ListAll()
        {
            List<LightingSatisfactionFactorViewModel> obj = new List<LightingSatisfactionFactorViewModel>();
            var temp = (from x in db.LightingSatisfactionFactor
                        where x.IsDelete == false
                        select new LightingSatisfactionFactorViewModel
                        {
                            PKey = x.PKey,
                            FactorName = x.FactorName,
                            Description = x.Description,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.FactorName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(LightingSatisfactionFactorViewModel obj)
        {
            int i = 1;
            try
            {
                LightingSatisfactionFactor model = new LightingSatisfactionFactor();
                model.PKey = Guid.NewGuid();
                model.FactorName = obj.FactorName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.LightingSatisfactionFactor.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(LightingSatisfactionFactorViewModel obj)
        {
            int i = 1;
            try
            {
                LightingSatisfactionFactor model = db.LightingSatisfactionFactor.Find(obj.PKey);
                model.FactorName = obj.FactorName;
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
                LightingSatisfactionFactor model = db.LightingSatisfactionFactor.Find(ID);
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