using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtCoolingEfficiencyType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<CoolingEfficiencyTypeClass> ListAll()
        {
            List<CoolingEfficiencyTypeClass> obj = new List<CoolingEfficiencyTypeClass>();
            var temp = (from x in db.CoolingEfficientyType
                        where x.IsDelete == false 
                        select new CoolingEfficiencyTypeClass
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(CoolingEfficiencyTypeClass obj)
        {
            int i = 1;
            try
            {
                CoolingEfficientyType model = new CoolingEfficientyType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.CoolingEfficientyType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(CoolingEfficiencyTypeClass obj)
        {
            int i = 1;
            try
            {
                CoolingEfficientyType model = db.CoolingEfficientyType.Find(obj.PKey);
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
                CoolingEfficientyType model = db.CoolingEfficientyType.Find(ID);
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