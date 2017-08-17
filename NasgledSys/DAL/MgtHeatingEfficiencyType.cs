using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasgledSys.DAL
{
    public class MgtHeatingEfficiencyType
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public List<HeatingEfficiencyTypeClass> ListAll()
        {
            List<HeatingEfficiencyTypeClass> obj = new List<HeatingEfficiencyTypeClass>();
            var temp = (from x in db.CoolingEfficientyType
                        where x.IsDelete == false
                        select new HeatingEfficiencyTypeClass
                        {
                            PKey = x.PKey,
                            Description = x.Description,
                            TypeName = x.TypeName,
                            IsDelete = x.IsDelete
                        }).OrderBy(m => m.TypeName);
            obj = temp.ToList();
            return obj;
        }

        public int Add(HeatingEfficiencyTypeClass obj)
        {
            int i = 1;
            try
            {
                HeatingEfficiencyType model = new HeatingEfficiencyType();
                model.PKey = Guid.NewGuid();
                model.TypeName = obj.TypeName;
                model.Description = obj.Description;
                model.IsDelete = false;
                db.HeatingEfficiencyType.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                i = 0;
            }
            return i;
        }

        public int Update(HeatingEfficiencyTypeClass obj)
        {
            int i = 1;
            try
            {
                HeatingEfficiencyType model = db.HeatingEfficiencyType.Find(obj.PKey);
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
                HeatingEfficiencyType model = db.HeatingEfficiencyType.Find(ID);
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